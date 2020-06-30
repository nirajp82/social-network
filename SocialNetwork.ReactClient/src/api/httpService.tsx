import axios, { AxiosResponse } from 'axios';
import createBrowserHistory from '../utils/createBrowserHistory';
import * as constants from '../utils/constants';
import { toast } from 'react-toastify';

const sleepTime = 1;

const axiosInstance = axios.create({
    baseURL: "http://localhost/socialnetwork/api/",
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

axiosInstance.interceptors.response.use((response) => response, (err) => {
    //const { response, config, data } = err;   
    const { response } = err;
    if (err.message === "Network Error" && !response) {
        toast.error('Network error server is down for maintenance, Please try after sometime');
        return;
    }
    switch (response.status) {
        case 400:
            toast.error('Bad request, Please check data');
            break;
        case 401:
            createBrowserHistory.push(constants.NAV_HOME);
            break;
        case 404:
            createBrowserHistory.push(constants.NAV_NOT_FOUND);
            break;
        case 500:
            toast.error('Server Issue - Oops, something went wrong');
            break;
    }
    throw err.response;
});

const processResponse = (dbResponse: AxiosResponse) => {
    return dbResponse.data;
};

const addDelay = (ms: number) => (dbResponse: AxiosResponse) => {
    return new Promise<AxiosResponse>(resolve => setTimeout(() => resolve(dbResponse), ms));
};

const httpService = {
    get: async (url: string) => {
        const dbResponse: AxiosResponse = await axiosInstance.get(url).then(addDelay(sleepTime));
        return processResponse(dbResponse);
    },
    post: async (url: string, body: {}) => {
        const dbResponse: AxiosResponse = await axiosInstance.post(url, body).then(addDelay(sleepTime));
        return processResponse(dbResponse);
    },
    put: async (url: string, body: {}) => {
        const dbResponse: AxiosResponse = await axiosInstance.put(url, body).then(addDelay(sleepTime));
        return processResponse(dbResponse);
    },
    delete: async (url: string) => {
        const dbResponse: AxiosResponse = await axiosInstance.delete(url).then(addDelay(sleepTime));
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