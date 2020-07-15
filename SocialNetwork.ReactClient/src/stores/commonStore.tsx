import { observable, action } from 'mobx';
import jwt from 'jsonwebtoken';

import { rootStore } from './rootStore';
import * as constants from '../utils/constants';
import { getRefreshToken } from '../api/httpService';
import { toast } from 'react-toastify';

class commonStore {
    rootStore: rootStore;

    constructor(rootStore: rootStore) {
        this.rootStore = rootStore;
    };

    @observable appLoaded: boolean = false;

    @action setToken(token: string | null) {
        if (token)
            window.localStorage.setItem(constants.AUTH_TOKEN_NAME, token);
        else
            window.localStorage.removeItem(constants.AUTH_TOKEN_NAME);
    }

    @action setRefreshToken(refreshToken: string | null) {
        if (refreshToken)
            window.localStorage.setItem(constants.AUTH_REFRESH_TOKEN_NAME, refreshToken);
        else
            window.localStorage.removeItem(constants.AUTH_REFRESH_TOKEN_NAME);
    }

    @action setAppLoaded() {
        this.appLoaded = true;
    }

    getOrRefreshToken = async () => {
        const token = this.getToken();
        const refreshToken = this.getRefreshToken();
        if (token && refreshToken) {
            const decodedToken: any = jwt.decode(token);
            //Get new token if current token is expired.
            if (decodedToken && decodedToken.exp * 1000 <= Date.now() + 5000) {
                try {
                    return await (await getRefreshToken(token, refreshToken)).token;
                } catch (err) {
                    toast.error('Problem getting refresh token!');
                }
            }
            //If token is not expired return it
            return token;
        }
        return '';
    };

    getToken = (): string | null => {
        return window.localStorage.getItem(constants.AUTH_TOKEN_NAME);
    };

    getRefreshToken = (): string | null => {
        return window.localStorage.getItem(constants.AUTH_REFRESH_TOKEN_NAME);
    };
};

export default commonStore;