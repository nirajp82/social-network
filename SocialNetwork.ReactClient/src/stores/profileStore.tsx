import { rootStore } from './rootStore';
import { action, computed, runInAction, observable, reaction } from 'mobx';
import { toast } from 'react-toastify';

import profileService from '../api/profileService';
import photoService from '../api/photoService';
import { IProfile, IPhoto } from '../models/IProfile';
import * as constants from '../utils/constants';
import { IUserActivity } from '../models/IActivity';

export default class profileStore {
    rootStore: rootStore;
    @observable userProfile: IProfile | null = null;
    @observable activeTabIndex: string | number | undefined = 0;
    @observable followers: IProfile[] | undefined = undefined;
    @observable isLoadingfollowers: boolean = false;
    @observable userActivities: IUserActivity[] | null = null;

    constructor(rootStore: rootStore) {
        this.rootStore = rootStore;

        reaction(() => this.activeTabIndex, index => {
            if (index === constants.TAB_INDEX_FOLLOWERS)
                this.loadFollowers(this.userProfile!.appUserId, constants.PREDICATE_FOLLOWERS);
            else if (index === constants.TAB_INDEX_FOLLOWINGS)
                this.loadFollowers(this.userProfile!.appUserId, constants.PREDICATE_FOLLOWINGS);
            else
                this.followers = undefined;
        });
    }

    @computed get isViewingOwnProfile(): boolean {
        return !!(this.rootStore.userStore.user?.userName === this.userProfile?.username);
    };

    @computed get isUserViewingFollowersTab(): boolean {
        return (this.activeTabIndex === constants.TAB_INDEX_FOLLOWERS);
    };

    @computed get isUserViewingFollowingTab(): boolean {
        return (this.activeTabIndex === constants.TAB_INDEX_FOLLOWINGS);
    };

    @action setActiveTab = (tabIndex: string | number | undefined) => {
        this.activeTabIndex = tabIndex;
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
                    this.rootStore.userStore.setMainPhoto(this.userProfile.mainPhoto);
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
                this.rootStore.userStore.setDisplayName(displayName);
                this.userProfile = { ...this.userProfile, ...aboutProfile };
                this.userProfile!.displayName = displayName;
            });
        } catch (error) {
            console.error(error);
            toast.error('Problem updating user profile');
        }
    };

    @action follow = async (userId: string) => {
        try {
            const user: IProfile = await profileService.follow(userId);
            runInAction(() => {
                this.userProfile!.followersCount += 1;
                this.userProfile!.following = true;
                if (this.isUserViewingFollowersTab) {
                    this.followers = this.followers ?? [];
                    this.followers.push(user);
                }
            });
        } catch (error) {
            console.error(error);
            toast.error('Problem following user');
        }
    };

    @action unfollow = async (userId: string) => {
        try {
            await profileService.unfollow(userId);
            runInAction(() => {
                this.userProfile!.followersCount -= 1;
                this.userProfile!.following = false;
                if (this.isUserViewingFollowersTab) {
                    this.followers = this.followers?.filter(u => u.appUserId !== this.rootStore.userStore.user?.appUserId);
                }
            });
        } catch (error) {
            console.error(error);
            toast.error('Problem unfollowing user');
        }
    };

    @action loadFollowers = async (userId: string, predicate: string) => {
        try {
            this.isLoadingfollowers = true;
            let followers: IProfile[] | undefined = undefined;

            if (predicate === constants.PREDICATE_FOLLOWERS)
                followers = await profileService.followers(userId);
            else
                followers = await profileService.followings(userId);

            runInAction(() => {
                this.followers = followers && followers.length > 0 ? followers : undefined;
                this.isLoadingfollowers = false;
            });
        } catch (error) {
            console.error(error);
            toast.error('Problem loading followers');
            this.isLoadingfollowers = false;
        }
        return null;
    };

    @action setUserActivities = (userActivities: IUserActivity[]) => {
        this.userActivities = userActivities;
    };

    @action loadUserActivities = async (appUserId: string, predicate: string) => {
        try {
            const qsParams = new URLSearchParams();
            qsParams.set('Predicate', predicate);
            const userActivities = await profileService.userActivities(appUserId, qsParams);
            this.setUserActivities(userActivities);
        } catch (error) {
            console.error(error);
            toast.error('Problem loading user activities');
        }
        return null;
    };
};

