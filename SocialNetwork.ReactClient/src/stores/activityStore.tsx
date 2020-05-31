import { observable, action, computed, configure, runInAction } from 'mobx';
import { createContext } from 'react';
import { IActivity } from '../models/IActivity';
import activityService from '../api/activities';

// don't allow state modifications outside actions
configure({ enforceActions: "always" })

class activityStore {
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

    @action setIsLoading = (value: boolean) => {
        this.isLoading = value;
    };

    @action setIsSaving = (value: boolean) => {
        this.isSaving = value;
    };

    @action setIsDeleting = (value: boolean) => {
        this.isDeleting = value;
    };

    @action loadActivities = async () => {
        this.setIsLoading(true);
        try {
            const activities = await activityService.list();
            runInAction(() => {
                activities.forEach((activity) => {
                    this.activityRegistry.set(activity.id, activity);
                })
            });
        } catch (error) {
            console.error(error);
        }
        finally {
            this.setIsLoading(false);
        }
    };

    @action loadActivity = async (id: string): Promise<IActivity | undefined> => {
        let activity = this.getActivity(id);
        if (activity)
            return activity;

        this.isLoading = true;
        try {
            activity = await activityService.details(id);
            if (activity) {
                const dbActivity = activity as unknown as IActivity;
                runInAction(() => {
                    this.activityRegistry.set(dbActivity.id, dbActivity);
                });
            }
        } catch (error) {
            console.error(error);
        }
        finally {
            this.setIsLoading(false);
        }
        return activity;
    };

    getActivity = (id: string): IActivity | undefined => {
        return this.activityRegistry.get(id);
    };

    @action createActivity = async (activity: IActivity) => {
        this.setIsSaving(true);

        try {
            activity.id = await activityService.create(activity);
            runInAction(() => {
                this.activityRegistry.set(activity.id, activity);
                this.setSelectActivity(activity.id);
            });
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
            runInAction(() => {
                this.activityRegistry.set(activity.id, activity);
                this.setSelectActivity(activity.id);
            });
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
            runInAction(() => {
                this.activityRegistry.delete(id);
                this.setSelectActivity("");
                this.setIsDeleting(false);
                this.setShowFormFlag(false);
            });
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