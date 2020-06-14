import { observable, action, computed } from 'mobx';

import { IUser, ILogin, IRegister } from '../models/IUser';
import userService from '../api/userService';
import { rootStore } from './rootStore';

class userStore {
    rootStore: rootStore;

    constructor(rootStore: rootStore) {
        this.rootStore = rootStore;
    }

    @observable user: IUser | null = null;

    @computed get isUserLoggedIn(): Boolean {
        return !!this.user;
    };

    @action setUser = (user: IUser) => {
        this.user = user;
    };

    @action login = async (command: ILogin) => {
        try {
            const user = await userService.login(command);
            this.setUser(user);
        } catch (err) {
            //console.log(err);
            throw err;
        }
    };

    @action register = async (command: IRegister) => {
        try {
            if (!this.isUserLoggedIn) {
                const user = await userService.register(command);
                this.setUser(user);
            }
        } catch (err) {
            console.log(err);
        }
    };
};

export default userStore;