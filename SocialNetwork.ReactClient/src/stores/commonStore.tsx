import { observable, action } from 'mobx';
import { rootStore } from './rootStore';
import * as constants from '../utils/constants';

class commonStore {
    rootStore: rootStore;

    constructor(rootStore: rootStore) {
        this.rootStore = rootStore;
        //reaction(() => this.token,
        //    (token) => {
        //        if (token)
        //            window.localStorage.setItem(constants.AUTH_TOKEN_NAME, token);
        //        else
        //            window.localStorage.removeItem(constants.AUTH_TOKEN_NAME);
        //    });
    };

    //@observable token: string | null = window.localStorage.getItem(constants.AUTH_TOKEN_NAME);
    @observable appLoaded: boolean = false;

    @action setToken(token: string | null) {
        //this.token = token;
        if (token)
            window.localStorage.setItem(constants.AUTH_TOKEN_NAME, token);
        else
            window.localStorage.removeItem(constants.AUTH_TOKEN_NAME);
    }

    @action setRefreshToken(refreshToken: string | null) {
        //this.token = token;
        if (refreshToken)
            window.localStorage.setItem(constants.AUTH_REFRESH_TOKEN_NAME, refreshToken);
        else
            window.localStorage.removeItem(constants.AUTH_REFRESH_TOKEN_NAME);
    }

    @action setAppLoaded() {
        this.appLoaded = true;
    }

    getToken = (): string | null => {
        return window.localStorage.getItem(constants.AUTH_TOKEN_NAME);
    };

    getRefreshToken = (): string | null => {
        return window.localStorage.getItem(constants.AUTH_REFRESH_TOKEN_NAME);
    };
};

export default commonStore;