export interface IUser {
    appUserId: string,
    displayName: string;
    userName: string;
    token: string;
    refreshToken: string;
    image: string;
};

export interface IRegister {
    firstName: string;
    lastName: string;
    email: string;
    userName: string;
    password: string;
};

export interface ILogin {
    userName: string;
    password: string;
};