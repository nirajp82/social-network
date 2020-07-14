import axios, { AxiosResponse } from 'axios';
import createBrowserHistory from '../utils/createBrowserHistory';
import * as constants from '../utils/constants';
import { toast } from 'react-toastify';

//const SLEEP_TIME = 100;

const axiosInstance = axios.create({
    baseURL: `${constants.BASE_SERVICE_URL}/api/`,
    withCredentials: true,
    timeout: 30000
});

axiosInstance.interceptors.request.use((config) => {
    //Add JWT Authorization Token to request if exists.
    const token: string | null = window.localStorage.getItem(constants.AUTH_TOKEN_NAME);
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});

axiosInstance.interceptors.response.use((response) => response, (error) => {
    if (error.message === 'Network Error' && !error.response) {
        toast.error('Network error server is down for maintenance, Please try after sometime');
        return;
    }
    const originalRequest = error.config;
    const { config, data, status } = error.response;
    if (status === 400) {
        if (config.method === 'get' && data.errors.hasOwnProperty('id')) {
            createBrowserHistory.push(constants.NAV_NOT_FOUND);
        }
        else {
            toast.error('Bad request, Please check data');
        }
    }
    else if (status === 401) {
        if (originalRequest.url.endsWith('refresh')) {
            window.localStorage.removeItem(constants.AUTH_TOKEN_NAME);
            window.localStorage.removeItem(constants.AUTH_REFRESH_TOKEN_NAME);
            createBrowserHistory.push(constants.NAV_LOGIN);
            toast.error('Your session has expired, please login again');
            return Promise.reject(error);
        }
        else if (!originalRequest._refreshAttempt) {
            originalRequest._refreshAttempt = true;
            return axios.post(`${constants.BASE_SERVICE_URL}/api/user/refresh`, {
                token: window.localStorage.getItem(constants.AUTH_TOKEN_NAME),
                refreshToken: window.localStorage.getItem(constants.AUTH_REFRESH_TOKEN_NAME)
            }).then(res => {
                window.localStorage.setItem(constants.AUTH_TOKEN_NAME, res.data.token);
                window.localStorage.setItem(constants.AUTH_REFRESH_TOKEN_NAME, res.data.refreshToken);
                axios.defaults.headers.common['Authorization'] = `Bearer ${res.data.token}`;
                originalRequest.headers['Authorization'] = axios.defaults.headers.common['Authorization'];
                return axios(originalRequest);
            });
        }
    }
    else if (status === 404) {
        createBrowserHistory.push(constants.NAV_NOT_FOUND);
    }
    else if (status === 500) {
        toast.error('Oops! Something went wrong, please try again later');
    }
    throw error.response;
});

const processResponse = (dbResponse: AxiosResponse) => {
    return dbResponse.data;
};

//const addDelay = (ms: number) => (dbResponse: AxiosResponse) => {
//    return new Promise<AxiosResponse>(resolve => setTimeout(() => resolve(dbResponse), ms));
//};

const httpService = {
    get: async (url: string, qsParams?: URLSearchParams) => {
        const dbResponse: AxiosResponse = await axiosInstance.get(url, { params: qsParams });
        //.then(addDelay(SLEEP_TIME));
        return processResponse(dbResponse);
    },
    post: async (url: string, body: {}) => {
        const dbResponse: AxiosResponse = await axiosInstance.post(url, body);
        //.then(addDelay(SLEEP_TIME));
        return processResponse(dbResponse);
    },
    put: async (url: string, body: {}) => {
        const dbResponse: AxiosResponse = await axiosInstance.put(url, body);
        //.then(addDelay(SLEEP_TIME));
        return processResponse(dbResponse);
    },
    delete: async (url: string) => {
        const dbResponse: AxiosResponse = await axiosInstance.delete(url);
        //.then(addDelay(SLEEP_TIME));
        return processResponse(dbResponse);
    },
    postForm: async (url: string, formData: FormData) => {
        const dbResponse: AxiosResponse = await axiosInstance.post(url, formData, {
            headers: { 'Content-type': 'multipart/form-data' }
        });
        return processResponse(dbResponse);
    }
};

export default httpService;