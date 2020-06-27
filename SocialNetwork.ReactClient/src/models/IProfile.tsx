export interface IProfile {
    displayName: string,
    username: string,
    mainPhoto: IPhoto | null,
    bio: string,
    // IsFollowed:string
    followersCount: string,
    followingCount: string
    photos: IPhoto[],
}

export interface IPhoto {
    id: string;
    url: string;
}