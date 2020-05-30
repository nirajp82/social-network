import { observable, action, computed } from 'mobx';
import { createContext } from 'react';
import { IActivity } from '../models/IActivity';
import activityService from '../api/activities';

class activityStore {
    //@observable activities: IActivity[] = [];
    @observable activityRegistry = new Map<string, IActivity>();
    @observable isLoading = false;
    @observable selectedActivity: IActivity | null = null;
    @observable showForm = false;
    @observable isSaving = false;
    @observable isDeleting = false;

    @action setSelectActivity = (id: string) => {
        if (id === "")
            this.selectedActivity = null;
        else
            this.selectedActivity = this.activityRegistry.get(id) as IActivity;
    };

    @action setShowFormFlag = (value: boolean) => {
        this.showForm = value;
    };

    @action setIsSaving = (value: boolean) => {
        this.isSaving = value;
    };

    @action setIsDeleting = (value: boolean) => {
        this.isDeleting = value;
    };

    @action fetchActivities = async () => {
        this.isLoading = true;
        const activities = await activityService.list().finally(() => {
            this.isLoading = false;
        });
        activities.forEach((activity) => {
            this.activityRegistry.set(activity.id, activity);
        });
    };

    @action createActivity = async (activity: IActivity) => {
        this.setIsSaving(true);

        try {
            activity.id = await activityService.create(activity);
            this.activityRegistry.set(activity.id, activity);
            //const newActivity: IActivity = { ...activity, id: newId };
            //this.setActivities([...this.activities, newActivity]);
            this.setSelectActivity(activity.id);
        } catch (error) {
            console.error(error);
        }
        finally {
            this.setIsSaving(false);
            this.setShowFormFlag(false);
        }
    };

    @action editActivity = async (activity: IActivity) => {
        this.setIsSaving(true);
        try {
            await activityService.update(activity);
            //this.setActivities([...this.activities.filter(a => a.id !== activity.id), activity]);
            this.activityRegistry.set(activity.id, activity);
            this.setSelectActivity(activity.id);
        } catch (error) {
            console.error(error);
        }
        finally {
            this.setIsSaving(false);
            this.setShowFormFlag(false);
        }
    };

    @action deleteActivity = async (id: string) => {
        this.setIsDeleting(true);
        try {
            await activityService.delete(id);
            //this.setActivities([...this.activities.filter(a => a.id !== id)]);
            this.activityRegistry.delete(id);
            this.setSelectActivity("");

        } catch (error) {
            console.error(error);
        }
        finally {
            this.setIsDeleting(false);
            this.setShowFormFlag(false);
        }
    };

    @computed get activityByDate(): IActivity[] {
        return Array.from(this.activityRegistry.values());
    };
};

export default createContext(new activityStore());