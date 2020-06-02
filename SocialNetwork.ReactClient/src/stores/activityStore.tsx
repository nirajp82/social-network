import { observable, action, computed, configure, runInAction } from 'mobx';
import { createContext } from 'react';
import { IActivity } from '../models/IActivity';
import activityService from '../api/activities';

// don't allow state modifications outside actions
configure({ enforceActions: "always" })

class activityStore {
    @observable activityRegistry = new Map<string, IActivity>();
    @observable isLoading = false;
    @observable showForm = false;
    @observable isSaving = false;
    @observable isDeleting = false;

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
        if (!id || (id && id.length === 0))
            return;

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

    @action createActivity = async (activity: IActivity): Promise<string> => {
        this.setIsSaving(true);

        try {
            activity.id = await activityService.create(activity);
            runInAction(() => {
                this.activityRegistry.set(activity.id, activity);
                return activity.id;
            });
        } catch (error) {
            console.error(error);
        }
        finally {
            this.setIsSaving(false);
            this.setShowFormFlag(false);
        }
        return "";
    };

    @action editActivity = async (activity: IActivity) => {
        this.setIsSaving(true);
        try {
            await activityService.update(activity);
            runInAction(() => {
                this.activityRegistry.set(activity.id, activity);
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

    @computed get activityByDate(): [string, IActivity[]][] {
        const sortedArray = Array.from(this.activityRegistry.values()).sort(
            (a, b): number => {
                if (a.date && b.date) {
                    return (new Date(a.date).getTime() - new Date(b.date).getTime());
                }
                else if (a)
                    return 1;
                return 0;
            }
        )
        return this.groupActivitiesByDate(sortedArray);
    };

    groupActivitiesByDate = (sortedArray: IActivity[]): [string, IActivity[]][] => {
        const initialValue: { [key: string]: IActivity[] } = {};

        return Object.entries(sortedArray.reduce((accumulator, currentValue) => {
            const date = currentValue.date?.toString().split('T')[0] as string;
            accumulator[date] = accumulator[date] ? [...accumulator[date], currentValue] : [currentValue];
            return accumulator;
        }, initialValue));
    };
};

export default createContext(new activityStore());