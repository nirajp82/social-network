import { observable, action, computed, toJS } from 'mobx';

import { IUser, ILogin, IRegister } from '../models/IUser';
import userService from '../api/userService';
import { rootStore } from './rootStore';
import * as constants from '../utils/constants';
import createBrowserHistory from '../utils/createBrowserHistory';

class userStore {
    rootStore: rootStore;

    constructor(rootStore: rootStore) {
        this.rootStore = rootStore;
    }

    @observable user: IUser | null = null;

    @computed get isUserLoggedIn(): Boolean {
        return !!this.user;
    };

    @action setUser = (user: IUser | null) => {
        this.user = user;
    };

    @action login = async (command: ILogin) => {
        try {
            const user = await userService.login(command);
            this.setUser(user);
            this.rootStore.commonStore.setToken(user.token);
        } catch (err) {
            throw err;
        }
    };

    @action register = async (command: IRegister) => {
        try {
            if (!this.isUserLoggedIn) {
                const user = await userService.register(command);
                this.setUser(user);
                this.rootStore.commonStore.setToken(user.token);
            }
        } catch (err) {
            throw err;
        }
    };

    @action current = async () => {
        try {
            if (!this.user) {
                const user = await userService.current();
                this.setUser(user);
            }
            return this.user;
        } catch (err) {
            console.log(err);
        }
    };

    getCurrentUserInstance = () => {
        return toJS(this.user);
    };

    @action logout = () => {
        this.rootStore.commonStore.setToken(null);
        this.setUser(null);
        createBrowserHistory.push(constants.NAV_HOME);
    };
};

export default userStore;