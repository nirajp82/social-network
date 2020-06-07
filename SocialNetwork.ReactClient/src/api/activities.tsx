import httpService from './httpService';

import { IActivity } from '../models/IActivity';


const activityService = {
    list: (): Promise<IActivity[]> => {
        return httpService.get("/activities");
    },
    details: async (id: string) => {
        return await httpService.get(`/activities/${id}`);
    },
    create: (activity: IActivity): Promise<string> => {
        //return httpService.post('/activities', activity) as unknown as string;
        return httpService.post('/activities', activity);
    },
    update: async (activity: IActivity) => {
        return await httpService.put(`/activities/${activity.id}`, activity);
    },
    delete: async (id: string) => {
        return await httpService.delete(`/activities/${id}`);
    }
};

export default activityService;