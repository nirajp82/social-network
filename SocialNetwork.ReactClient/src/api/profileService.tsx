import { IProfile } from "../models/IProfile";
import httpService from "./httpService";

const profileService = {
    get: (appUserId: string): Promise<IProfile> => {
        return httpService.get(`profile/${appUserId}`);
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