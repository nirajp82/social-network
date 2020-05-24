import httpService from './agent';

import { IActivity } from '../models/IActivity';


const activityService = {
    list: () => {
        return httpService.get("/activities");
    },
    details: (id: string) => {
        return httpService.get(`/activities/${id}`);
    },
    create: (activity: IActivity): string => {
        return httpService.post('/activities', activity) as unknown as string;
    },
    update: (activity: IActivity) => {
        return httpService.put(`/activities/${activity.id}`, activity);
    },
    delete: (id: string) => {
        return httpService.delete(`/activities/${id}`);
    }
};

export default activityService;