import { IProfile } from "../models/IProfile";
import httpService from "./httpService";

const profileService = {
    get: (appUserId: string): Promise<IProfile> => {
        return httpService.get(`Profile/${appUserId}`);
    }
};

export default profileService;