export interface IActivity {
    id: string;
    title: string;
    description: string;
    category: string;
    date: Date;
    city: string;
    venue: string;
    host: IAttendee | null;
    isCurrentUserGoing: boolean;
    isCurrentUserHost: boolean;
    attendees: IAttendee[];
    comments: IComment[];
};

export interface IComment {
    id: string;
    body: string;
    createdAt: Date;
    userId: string;
    userDisplayName: string;
    userImage: string;
}

export interface IAttendee {
    appUserId: string,
    displayName: string,
    image: string,
    isHost: boolean,
    following?: boolean
};

//following "extends Partial<IActivity>" line will inherit all the properties from IActivity and make all them optional 
export interface IActivityFormValues extends Partial<IActivity> {
    time?: Date
}

export class ActivityFormValues implements IActivityFormValues {
    id?: string = undefined;
    title: string = '';
    description: string = '';
    category: string = '';
    date?: Date = undefined;
    time?: Date = undefined;
    city: string = '';
    venue: string = '';

    constructor(value?: IActivityFormValues) {
        Object.assign(this, value);
        if (value && value.date)
            this.time = value.date;
    }
}