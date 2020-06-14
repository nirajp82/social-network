import httpService from './httpService';
import { IUser, ILogin, IRegister } from '../models/IUser';

const userService = {
    current: (): Promise<IUser> => {
        return httpService.get('/user');
    },
    login: (command: ILogin): Promise<IUser> => {
        return httpService.post('/user/login', command);
    },
    register: (command: IRegister): Promise<IUser> => {
        return httpService.post('/user/register', command);
    }
};

export default userService;