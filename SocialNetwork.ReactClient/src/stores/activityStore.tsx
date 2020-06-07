import { observable, action, computed, configure, runInAction } from 'mobx';
import { createContext } from 'react';
import moment from 'moment';

import { IActivity } from '../models/IActivity';
import activityService from '../api/activities';

// don't allow state modifications outside actions
configure({ enforceActions: "always" })

class activityStore {
    @observable activityRegistry = new Map<string, IActivity>();
    @observable isLoadingActivities = false;
    @observable isLoadingActivity = false;
    @observable showForm = false;
    @observable isSaving = false;
    @observable isDeleting = false;
    //@observable selectedActivity: IActivity | undefined;

    getActivity = (id: string): IActivity | undefined => {
        return this.activityRegistry.get(id);
    };

    @action setShowFormFlag = (value: boolean) => {
        this.showForm = value;
    };

    @action setIsLoadingActivities = (value: boolean) => {
        this.isLoadingActivities = value;
    };

    @action setIsLoadingActivity = (value: boolean) => {
        this.isLoadingActivity = value;
    };

    @action setIsSaving = (value: boolean) => {
        this.isSaving = value;
    };

    @action setIsDeleting = (value: boolean) => {
        this.isDeleting = value;
    };

    @action setActivity = (activity: IActivity) => {
        activity.date = new Date(activity.date);
        this.activityRegistry.set(activity.id, activity);
    };

    @action loadActivities = async () => {
        this.setIsLoadingActivities(true);
        try {
            const activities = await activityService.list();
            activities.forEach((activity) => {
                this.setActivity(activity);
            })
            this.setIsLoadingActivities(false);
        } catch (error) {
            console.error(error);
            this.setIsLoadingActivities(false);
        }
    };

    @action loadActivity = async (id: string) => {
        if (!id || (id && id.length === 0))
            return;

        let activity = this.getActivity(id);
        if (activity)
            return activity;

        this.setIsLoadingActivity(true);
        try {
            activity = await activityService.details(id);
            this.setActivity(activity!);
            this.setIsLoadingActivity(false);
            return activity;
        } catch (error) {
            console.error(error);
            this.setIsLoadingActivity(false);
        }
    };

    @action createActivity = async (activity: IActivity): Promise<string> => {
        this.setIsSaving(true);

        try {
            activity.id = await activityService.create(activity);
            this.setActivity(activity);
            this.setIsSaving(false);
            this.setShowFormFlag(false);
            return activity.id;
        } catch (error) {
            console.error(error);
            this.setIsSaving(false);
            this.setShowFormFlag(false);
        }
        return '';
    };

    @action editActivity = async (activity: IActivity): Promise<boolean> => {
        this.setIsSaving(true);
        try {
            await activityService.update(activity);
            this.setActivity(activity);
            this.setIsSaving(false);
            this.setShowFormFlag(false);
            return true;
        } catch (error) {
            console.error(error);
            this.setIsSaving(false);
            this.setShowFormFlag(false);
        }
        return false;
    };

    @action deleteActivity = async (id: string) => {
        this.setIsDeleting(true);
        try {
            await activityService.delete(id);
            runInAction(() => {
                this.activityRegistry.delete(id);
            });
            this.setIsDeleting(false);
            this.setShowFormFlag(false);
        } catch (error) {
            console.error(error);
            this.setIsDeleting(false);
            this.setShowFormFlag(false);
        }
    };

    @computed get activityByDate(): [string, IActivity[]][] {
        const sortedArray = Array.from(this.activityRegistry.values()).sort(
            (a, b): number => {
                if (a.date && b.date) {
                    return a.date.getTime() - b.date.getTime();
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
            const date = moment(currentValue.date).format("MM-DD-yyyy");
            accumulator[date] = accumulator[date] ? [...accumulator[date], currentValue] : [currentValue];
            return accumulator;
        }, initialValue));
    };
};

export default createContext(new activityStore());