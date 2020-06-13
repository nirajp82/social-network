export interface IUser {
    DisplayName: String;
    UserName: String;
    Token: String;
    Image: String;
};

export interface IRegister {
    FirstName: String;
    LastName: String;
    Email: String;
    UserName: String;
    Password: String;
};

export interface ILogin {
    UserName: String;
    Password: String;
}