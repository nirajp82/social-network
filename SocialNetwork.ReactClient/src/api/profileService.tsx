import { IProfile } from "../models/IProfile";
import httpService from "./httpService";

const profileService = {
    get: (appUserId: string): Promise<IProfile> => {
        return httpService.get(`Profile/${appUserId}`);
    },
    update: (profile: IProfile): Promise<string> => {
        return httpService.put('Profile/', profile);
    }
};

export default profileService;