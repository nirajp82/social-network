import { rootStore } from './rootStore';
import profileService from '../api/profileService';
import { action, computed } from 'mobx';
import { IProfile } from '../models/IProfile';

export default class profileStore {
    _rootStore: rootStore;
    isLoadingProfile: boolean = true;
    userProfile: IProfile | null = null;

    constructor(rootStore: rootStore) {
        this._rootStore = rootStore;
    }

    @computed get isUserViewingOwnProfile(): boolean {
        return !!(this._rootStore.userStore.user?.userName === this.userProfile?.username);
    };

    @action setLoadingProfile = (isLoading: boolean) => {
        this.isLoadingProfile = isLoading;
    };

    @action setUserProfile = (userProfile: IProfile) => {
        this.userProfile = userProfile;
    };

    @action getUserProfile = async (appUserId: string) => {
        try {
            this.setLoadingProfile(true);
            const profile = await profileService.get(appUserId);
            this.setUserProfile(profile);
            this.setLoadingProfile(false);
            return profile;
        } catch (error) {
            console.error(error);
            this.setLoadingProfile(false);
        }
    };
};

