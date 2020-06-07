export interface IActivity {
    id: string;
    title: string;
    description: string;
    category: string;
    date: Date;
    city: string;
    venue: string;
}

//following "extends Partial<IActivity>" line will inherit all the properties from IActivity and make all them optional 
export interface IActivityFormValues extends Partial<IActivity> {
    time?: Date
}