export interface IUser {
    DisplayName: string;
    UserName: string;
    Token: string;
    Image: string;
};

export interface IRegister {
    FirstName: string;
    LastName: string;
    Email: string;
    UserName: string;
    Password: string;
};

export interface ILogin {
    UserName: string;
    Password: string;
}