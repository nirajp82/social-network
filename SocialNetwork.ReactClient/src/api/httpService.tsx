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
    const { status, headers } = error.response;
    if (status === 400)
        toast.error('Bad request, Please check data');
    else if (status === 401) {
        window.localStorage.removeItem(constants.AUTH_TOKEN_NAME);
        if (headers['www-authenticate'] && headers['www-authenticate'].indexOf('invalid_token') >= 0) {
            createBrowserHistory.push(constants.NAV_LOGIN);
            toast.info('Your session has expired, please login again');
        }
    }
    else if (status === 404) {
        createBrowserHistory.push(constants.NAV_NOT_FOUND);
    }
    else if (status === 500) {
        toast.error('Server Issue - Oops, something went wrong');
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