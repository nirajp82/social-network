import { observable, action, computed, runInAction } from 'mobx';
import moment from 'moment';
import { toast } from 'react-toastify';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

import { IActivity } from '../models/IActivity';
import activityService from '../api/activityService';
import { rootStore } from './rootStore';
import { isUserGoing, isUserHost, getHost, createAttendee, removeAttendee } from '../features/activities/util';

export default class activityStore {
    rootStore: rootStore;

    constructor(rootStore: rootStore) {
        this.rootStore = rootStore;
    }

    @observable activityRegistry = new Map<string, IActivity>();
    @observable selectedActivity: IActivity | null = null;
    @observable isLoadingActivities = false;
    @observable isLoadingActivity = false;
    @observable showForm = false;
    @observable isSaving = false;
    @observable isDeleting = false;
    @observable.ref hubConnection: HubConnection | null = null;

    @action createHubConnection = () => {
        //Build Hub Connection
        this.hubConnection = new HubConnectionBuilder()
            .withUrl('http://localhost/socialnetwork/chat', {
                //Send token as part as QueryString.
                accessTokenFactory: () => this.rootStore.commonStore.token!
            })
            .configureLogging(LogLevel.Information)
            .build();

        //Start Hub Connection.
        this.hubConnection.start()
            .then(() => this.hubConnection?.state!)
            .catch(error => console.error("Error establishing a connection", error));

        //Event Handlers on Receiving message from server.
        this.hubConnection.on('ReceiveComment', comment => {
            runInAction(() => {
                this.selectedActivity?.comments.push(comment)
            });
        });
    };

    @action stopHubConnection = () => {
        this.hubConnection!.stop();
    };

    @action addComment = async (comment: any) => {
        comment.activityId = this.selectedActivity!.id;
        try {
            await this.hubConnection!.invoke("SendComment", comment);
        } catch (error) {
            console.error(error);
        }
    };

    getActivity = (id: string): IActivity | undefined => {
        return this.activityRegistry.get(id);
    };

    @action setSelectedActivity = (activity: IActivity) => {
        this.selectedActivity = activity;
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
        const user = this.getCurrentUser();
        if (user) {
            activity.isCurrentUserGoing = isUserGoing(activity, user);
            activity.isCurrentUserHost = isUserHost(activity, user);
            activity.host = getHost(activity);
            activity.comments = activity.comments ?? [];
        }
        this.activityRegistry.set(activity.id, activity);
    };

    @action loadActivities = async () => {
        this.setIsLoadingActivities(true);
        try {
            const activities = await activityService.list();
            activities.forEach((activity) => {
                this.setActivity(activity);
            });
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
        if (activity) {
            this.setSelectedActivity(activity);
            return activity;
        }

        this.setIsLoadingActivity(true);
        try {
            activity = await activityService.details(id);
            this.setActivity(activity!);
            this.setSelectedActivity(activity);
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

    @action attend = async (activity: IActivity) => {
        try {
            await activityService.attend(activity.id);
            runInAction(() => {
                activity.attendees = activity.attendees || [];
                activity.attendees.push(createAttendee(this.getCurrentUser()!, false));
            });
            this.setActivity(activity);
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
            this.setActivity(activity);
        } catch (error) {
            console.error(error);
            toast.error('Problem cancelling attendance, Please try again later!');
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

    getCurrentUser = () => {
        return this.rootStore.userStore.getCurrentUserInstance();
    };
};

