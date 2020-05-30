import httpService from './agent';

import { IActivity } from '../models/IActivity';


const activityService = {
    list: (): Promise<IActivity[]> => {
        return httpService.get("/activities");
    },
    details: (id: string) => {
        return httpService.get(`/activities/${id}`);
    },
    create: (activity: IActivity): Promise<string> => {
        //return httpService.post('/activities', activity) as unknown as string;
        return httpService.post('/activities', activity);
    },
    update: (activity: IActivity) => {
        return httpService.put(`/activities/${activity.id}`, activity);
    },
    delete: (id: string) => {
        return httpService.delete(`/activities/${id}`);
    }
};

export default activityService;