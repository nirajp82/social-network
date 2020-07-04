//export interface IAboutProfile {
//    firstName: string;
//    lastName: string;
//    email: string;
//    bio: string;
//};

export interface IProfile {
    appUserId: string,
    displayName: string,
    firstName: string,
    lastName: string,
    email: string,
    bio: string,
    username: string,
    following: boolean,
    followersCount: number,
    followingCount: number,
    mainPhoto: IPhoto | null,
    photos: IPhoto[],
}

export interface IPhoto {
    id: string;
    url: string;
}