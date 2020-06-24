import { rootStore } from './rootStore';
import profileService from '../api/profileService';

export default class profileStore {
    _rootStore: rootStore;

    constructor(rootStore: rootStore) {
        this._rootStore = rootStore;
    }

    getUserProfile = (appUserId: string) => {
        return profileService.get(appUserId);
    };
};

