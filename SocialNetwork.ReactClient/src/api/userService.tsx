import httpService from './httpService';
import { IUser, ILogin, IRegister } from '../models/IUser';

const userService = {
    current: (): Promise<IUser> => {
        return httpService.get('/User/Current');
    },
    login: (command: ILogin): Promise<IUser> => {
        return httpService.post('/User/Login', command);
    },
    register: (command: IRegister): Promise<IUser> => {
        return httpService.post('/User/Register', command);
    }
};

export default userService;