import { rootStore } from './rootStore';
import { action, computed, runInAction } from 'mobx';

import profileService from '../api/profileService';
import photoService from '../api/photoService';
import { IProfile, IPhoto } from '../models/IProfile';
import { toast } from 'react-toastify';

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

    @action uploadPhoto = async (file: Blob): Promise<IPhoto | undefined> => {
        try {
            const photo = await photoService.upload(file);
            runInAction(() => {
                this.userProfile?.photos.push(photo);
            });
            return photo;
        } catch (error) {
            console.error(error);
            toast.error('Error uploading photo');
        }
    };

    @action setMain = async (photoId: string): Promise<void> => {
        try {
            await photoService.setMain(photoId);
            //@runInAction(() => {
            //    this.userProfile?.mainPhoto = 
            //});
        } catch (error) {
            console.error(error);
        }
    };

    @action deletePhoto = async (photoId: string): Promise<void> => {
        try {
            await photoService.delete(photoId);
        } catch (error) {
            console.error(error);
        }
    }
};

