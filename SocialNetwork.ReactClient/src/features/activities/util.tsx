import { IActivity, IAttendee } from '../../models/IActivity';
import { IUser } from '../../models/IUser';

//export const getHostName = (activity: IActivity): string => {
//    if (activity != null && activity.attendees.length > 0) {
//        const hosts = activity.attendees?.filter((attendee) => attendee.isHost);
//        const hostName = hosts.length > 0 ? hosts[0].displayName : '';
//        return hostName;
//    }
//    return '';
//};

export const isUserGoing = (activity: IActivity, user: IUser): boolean => {
    if (activity != null && activity.attendees.length > 0 && user) {
        return activity.attendees.some((attendee) => attendee.appUserId === user.appUserId);
    }
    return false;
};

export const isUserHost = (activity: IActivity, user: IUser): boolean => {
    if (activity != null && activity.attendees.length > 0 && user) {
        return activity.attendees.some((attendee) => attendee.appUserId === user.appUserId && attendee.isHost === true);
    }
    return false;
};

export const getHost = (activity: IActivity): IAttendee | null => {
    if (activity != null && activity.attendees.length > 0) {
        const hosts = activity.attendees.filter((attendee) => attendee.isHost === true);
        if (hosts.length > 0)
            return hosts[0];
    }

    return null;
};

export const createAttendee = (user: IUser, isHost: boolean): IAttendee => {
    return {
        appUserId: user.appUserId,
        displayName: user.displayName,
        image: user.image,
        isHost: isHost
    };
};

export const removeAttendee = (activity: IActivity, user: IUser) => {
    if (activity != null && activity.attendees.length > 0) {
        return activity.attendees.filter((attendee) => attendee.appUserId !== user.appUserId);
    };
    return [];
};