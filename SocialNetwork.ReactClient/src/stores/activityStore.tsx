import { observable, action, computed, runInAction, reaction } from 'mobx';
import moment from 'moment';
import { toast } from 'react-toastify';

import { IActivity, IComment } from '../models/IActivity';
import activityService from '../api/activityService';
import { rootStore } from './rootStore';
import { isUserGoing, isUserHost, getHost, createAttendee, removeAttendee } from '../features/activities/util';
import * as constants from '../utils/constants';

const PAGE_SIZE: number = 2;
export default class activityStore {
    rootStore: rootStore;

    constructor(rootStore: rootStore) {
        this.rootStore = rootStore;

        reaction(() => this.predicate.keys(), () => {
            this.currentPageNumber = 0;
            this.activityRegistry.clear();
            this.loadActivities();
        });
    }

    @observable activityRegistry = new Map<string, IActivity>();
    @observable selectedActivity: IActivity | null = null;
    @observable isLoadingActivity = false;
    @observable showForm = false;
    @observable isSaving = false;
    @observable isDeleting = false;

    //Paging and Filter
    @observable totalActivitiesCount = 0;
    @observable currentPageNumber = 0;
    @observable predicate = new Map();

    getActivity = (id: string): IActivity | undefined => {
        return this.activityRegistry.get(id);
    };

    @action setSelectedActivity = (activity: IActivity) => {
        this.selectedActivity = activity;
    };

    @action setShowFormFlag = (value: boolean) => {
        this.showForm = value;
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

    @action registerActivity = (activity: IActivity) => {
        activity.date = new Date(activity.date);
        const user = this.getCurrentUser();
        if (user) {
            activity.isCurrentUserGoing = isUserGoing(activity, user);
            activity.isCurrentUserHost = isUserHost(activity, user);
            activity.host = getHost(activity);
        }
        this.activityRegistry.set(activity.id, activity);
    };

    @action loadActivities = async () => {
        try {
            const { count, activities } = await activityService.list(this.getQSParams());
            if (activities) {
                activities.forEach((activity: IActivity) => {
                    this.registerActivity(activity);
                });
            }
            this.setTotalActivityCount(count);
        } catch (error) {
            console.error(error);
        }
    };

    @action loadActivity = async (id: string) => {
        if (!id || (id && id.length === 0))
            return;

        let activity: IActivity | undefined = this.getActivity(id);
        if (activity) {
            this.setSelectedActivity(activity);
            return activity;
        }

        this.setIsLoadingActivity(true);
        try {
            activity = await activityService.details(id);
            this.registerActivity(activity!);
            this.setSelectedActivity(activity!);
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
            runInAction(() => {
                activity.attendees = activity.attendees || [];
                activity.attendees.push(createAttendee(this.getCurrentUser()!, true));
            });
            this.registerActivity(activity);
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
            this.registerActivity(activity);
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

    @action attend = async (activity: IActivity) => {
        try {
            await activityService.attend(activity.id);
            runInAction(() => {
                activity.attendees = activity.attendees || [];
                activity.attendees.push(createAttendee(this.getCurrentUser()!, false));
            });
            this.registerActivity(activity);
        } catch (error) {
            console.error(error);
            toast.error('Problem signin up to activity, Please try again later!');
        }
    };

    @action unattend = async (activity: IActivity) => {
        try {
            await activityService.unattend(activity.id);
            runInAction(() => {
                activity.attendees = removeAttendee(activity, this.getCurrentUser()!);
            });
            this.registerActivity(activity);
        } catch (error) {
            console.error(error);
            toast.error('Problem cancelling attendance, Please try again later!');
        }
    };

    @action getComments = async () => {
        try {
            if (this.selectedActivity && this.selectedActivity.comments)
                return;

            const comments = await activityService.getComments(this.selectedActivity!.id);
            runInAction(() => {
                if (this.selectedActivity)
                    this.selectedActivity.comments = comments || [];
            });
        } catch (error) {
            console.error(error);
            toast.error('Problem fetching comments, Please try again later!');
        }
    };

    @action addComment = async (comment: any) => {
        comment.activityId = this.selectedActivity!.id;
        try {
            await this.rootStore.activityHubStore.sendComment(comment);
        } catch (error) {
            console.error(error);
        }
    };

    @action onReceivingCommentFromServer = (comment: IComment) => {
        this.selectedActivity?.comments.push(comment)
    };

    @action setTotalActivityCount = (count: number) => {
        this.totalActivitiesCount = count;
    };

    @action setPageNumber = (pageNumber: number) => {
        this.currentPageNumber = pageNumber;
    };

    @action setPredicate = (key: string, value: string | Date) => {
        this.predicate.clear();
        if (key !== constants.PREDICATE_ALL)
            this.predicate.set(key, value);
    }

    getQSParams = (): URLSearchParams => {
        const params = new URLSearchParams();
        params.append('offset', (this.currentPageNumber * PAGE_SIZE).toString());
        params.append('limit', PAGE_SIZE.toString());

        this.predicate.forEach((value, key) => {
            if (key === constants.PREDICATE_START_DATE)
                params.append(key, (value as Date).toISOString());
            else
                params.append(key, value.toString());
        });
        return params;
    }

    @computed get totalPages(): number {
        return Math.ceil(this.totalActivitiesCount / PAGE_SIZE);
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

    getCurrentUser = () => {
        return this.rootStore.userStore.getCurrentUserInstance();
    };
};

