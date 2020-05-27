import { observable, action } from 'mobx';
import { createContext } from 'react';
import { IActivity } from '../models/IActivity';
import activityService from '../api/activities';

class activityStore {
    @observable activities: IActivity[] = [];
    @observable isLoading = false;
    @observable selectedActivity: IActivity | null = null;
    @observable showForm = false;
    @observable isSaving = false;
    @observable isDeleting = false;

    @action fetchActivities = async () => {
        this.isLoading = true;
        this.activities = await activityService.list().finally(() => {
            this.isLoading = false;
        });
    };

    @action setActivities = (value: IActivity[]) => {
        this.activities = value;
    };

    @action setSelectActivity = (id: string) => {
        if (id === "")
            this.selectedActivity = null;
        else
            this.selectedActivity = this.activities.filter(a => a.id === id)[0];
    };

    @action setShowFormFlag = (value: boolean) => {
        this.showForm = value;
    };

    @action setIsSaving = (value: boolean) => {
        this.isSaving = value;
    };

    @action setIsDeleting = (value: boolean) => {
        this.isDeleting = value;
    }

    @action createActivity = async (activity: IActivity) => {
        this.setIsSaving(true);

        try {
            const newId: string = await activityService.create(activity);
            const newActivity: IActivity = { ...activity, id: newId };
            this.setActivities([...this.activities, newActivity]);
            this.setSelectActivity(newActivity.id);
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
            this.setActivities([...this.activities.filter(a => a.id !== activity.id), activity]);
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
            this.setActivities([...this.activities.filter(a => a.id !== id)]);
            this.setSelectActivity("");

        } catch (error) {
            console.error(error);
        }
        finally {
            this.setIsDeleting(false);
            this.setShowFormFlag(false);
        }
    };
};

export default createContext(new activityStore());