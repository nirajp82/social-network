import httpService from './httpService';

import { IActivity } from '../models/IActivity';


const activityService = {
    list: (offSet: number, limit: number) => {
        return httpService.get(`/activities?offset=${offSet}&limit=${limit}`);
    },
    details: (id: string) => {
        return httpService.get(`/activities/${id}`);
    },
    create: (activity: IActivity) => {
        //return httpService.post('/activities', activity) as unknown as string;
        return httpService.post('/activities', activity);
    },
    update: (activity: IActivity) => {
        return httpService.put(`/activities/${activity.id}`, activity);
    },
    delete: (id: string) => {
        return httpService.delete(`/activities/${id}`);
    },
    attend: (id: string) => {
        return httpService.post(`/activities/${id}/attend`, {});
    },
    unattend: (id: string) => {
        return httpService.post(`/activities/${id}/unattend`, {});
    },
    getComments: (id: string) => {
        return httpService.get(`/activities/${id}/comments`);
    }
};

export default activityService;