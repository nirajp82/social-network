import { IProfile } from "../models/IProfile";
import httpService from "./httpService";
import { IUserActivity } from "../models/IActivity";

const profileService = {
    get: (appUserId: string): Promise<IProfile> => {
        return httpService.get(`profile/${appUserId}`);
    },
    userActivities: (appUserId: string, qsParam: URLSearchParams): Promise<IUserActivity[]> => {
        return httpService.get(`profile/${appUserId}/activities`, qsParam);
    },
    followers: (userId: string): Promise<IProfile[]> => {
        return httpService.get(`profile/${userId}/followers`);
    },
    followings: (userId: string): Promise<IProfile[]> => {
        return httpService.get(`profile/${userId}/followings`);
    },
    update: (profile: IProfile): Promise<string> => {
        return httpService.put('profile/', profile);
    },
    follow: (userId: string) => {
        return httpService.post(`profile/${userId}/follow`, {});
    },
    unfollow: (userId: string) => {
        return httpService.post(`profile/${userId}/unfollow`, {});
    }
};

export default profileService;