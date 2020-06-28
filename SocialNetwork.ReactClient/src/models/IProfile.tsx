//export interface IAboutProfile {
//    firstName: string;
//    lastName: string;
//    email: string;
//    bio: string;
//};

export interface IProfile {
    displayName: string,
    firstName: string,
    lastName: string,
    email: string,
    bio: string,
    username: string,
    // IsFollowed:string
    followersCount: number,
    followingCount: number,
    mainPhoto: IPhoto | null,
    photos: IPhoto[],
}

export interface IPhoto {
    id: string;
    url: string;
}