import React, { useEffect, useState } from 'react';

import { IActivity } from '../models/IActivity';
import activityService from '../api/activities';

import NavBar from '../features/nav/NavBar';
import ActivityDashboard from '../features/activities/dashboard/ActivityDashboard';
import { Container } from 'semantic-ui-react';
import ProgressBar from './ProgressBar';



const App = () => {
    const [activities, setActivities] = useState<IActivity[]>([]);
    const [selectedActivity, setSelectedActivity] = useState<IActivity | null>(null);
    const [isEditMode, setEditMode] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [isSaving, setIsSaving] = useState(false);
    const [isDeleting, setIsDeleting] = useState(false);

    const selectActivity = (id: string) => {
        if (id === "") {
            setSelectedActivity(null);
        }
        else {
            let act = activities.filter(a => a.id === id)[0];
            setSelectedActivity(act);
        }
        updateEditMode(false);
    };

    const updateEditMode = (value: boolean) => {
        setEditMode(value);
    };

    const onCreateActivity = () => {
        selectActivity("");
        updateEditMode(true);
    };

    const fetchActivies = async (): Promise<IActivity[]> => {
        return await activityService.list();
    }

    const createActivityHandler = async (activity: IActivity) => {
        setIsSaving(true);
        const newId: string = await activityService.create(activity);
        const newActivity: IActivity = { ...activity, id: newId };
        setActivities([...activities, newActivity]);
        selectActivity(newActivity.id);
        setIsSaving(false);
    };

    const editActivityHandler = async (activity: IActivity) => {
        setIsSaving(true);
        await activityService.update(activity);
        setActivities([...activities.filter(a => a.id !== activity.id), activity]);
        selectActivity(activity.id);
        setSelectedActivity(activity);
        setIsSaving(false);
    };

    const deleteActivityHandler = async (id: string) => {
        setIsDeleting(true);
        await activityService.delete(id);
        setActivities([...activities.filter(a => a.id !== id)]);
        setIsDeleting(false);
    };

    useEffect(() => {
        const fetch = async () => {
            setActivities(await fetchActivies());
            setIsLoading(false);
        }
        fetch();
    }, []);

    if (isLoading) {
        return <ProgressBar message="Loading Activities"></ProgressBar>;
    }
    return (
        <React.Fragment>
            <NavBar onCreateActivity={onCreateActivity} />
            <Container style={{ marginTop: '7em' }}>
                <ActivityDashboard activities={activities}
                    selectActivity={selectActivity}
                    selectedActivity={selectedActivity}
                    isEditMode={isEditMode}
                    setEditMode={updateEditMode}
                    createActivityHandler={createActivityHandler}
                    editActivityHandler={editActivityHandler}
                    deleteActivityHandler={deleteActivityHandler}
                    isSaving={isSaving}
                    isDeleting={isDeleting}
                />
            </Container>
        </React.Fragment>
    );
};

export default App;