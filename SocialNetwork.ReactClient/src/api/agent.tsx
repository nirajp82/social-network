import axios, { AxiosResponse } from 'axios';
import createBrowserHistory from '../utils/createBrowserHistory';

const sleepTime = 10;

const axiosInstance = axios.create({
    baseURL: "http://localhost/socialnetwork/api/",
    withCredentials: true,
    timeout: 30000
});

axios.interceptors.response.use(undefined, err => {
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
    }
};

export default httpService;