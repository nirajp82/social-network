import { rootStore } from './rootStore';
import { action, computed, runInAction, observable } from 'mobx';
import { toast } from 'react-toastify';

import profileService from '../api/profileService';
import photoService from '../api/photoService';
import { IProfile, IPhoto } from '../models/IProfile';

export default class profileStore {
    _rootStore: rootStore;
    @observable userProfile: IProfile | null = null;

    constructor(rootStore: rootStore) {
        this._rootStore = rootStore;
    }

    @computed get isUserViewingOwnProfile(): boolean {
        return !!(this._rootStore.userStore.user?.userName === this.userProfile?.username);
    };

    @action setUserProfile = (userProfile: IProfile) => {
        this.userProfile = userProfile;
    };

    @action getUserProfile = async (appUserId: string) => {
        try {
            const profile = await profileService.get(appUserId);
            this.setUserProfile(profile);
        } catch (error) {
            console.error(error);
            toast.error('Error loading user profile');
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

    @action setMainPhoto = async (photoId: string): Promise<void> => {
        try {
            await photoService.setMain(photoId);
            runInAction(() => {
                if (this.userProfile) {
                    this.userProfile.mainPhoto = this.userProfile?.photos?.filter((p) => p.id === photoId)[0];
                    this._rootStore.userStore.setMainPhoto(this.userProfile.mainPhoto);
                }
            });
        } catch (error) {
            console.error(error);
            toast.error('Problem setting photo as main');
        }
    };

    @action deletePhoto = async (photoId: string): Promise<void> => {
        try {
            await photoService.delete(photoId);
            runInAction(() => {
                this.userProfile!.photos = this.userProfile!.photos!.filter((p) => p.id !== photoId);
            });
        } catch (error) {
            console.error(error);
            toast.error('Problem deleting photo');
        }
    }

    @action updateProfile = async (aboutProfile: IProfile) => {
        try {
            const displayName = await profileService.update(aboutProfile);
            runInAction(() => {
                this.userProfile!.displayName = displayName;
                this._rootStore.userStore.setDisplayName(displayName);
            });
        } catch (error) {
            console.error(error);
            toast.error('Problem updating user profile');
        }
    };
};

