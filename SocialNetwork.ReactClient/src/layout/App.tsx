import React, { useEffect, useState } from 'react';

import { IActivity } from '../models/IActivity';
import activityService from '../api/activities';

import NavBar from '../features/nav/NavBar';
import ActivityDashboard from '../features/activities/dashboard/ActivityDashboard';
import { Container } from 'semantic-ui-react';


const App = () => {
    const [activities, setActivities] = useState<IActivity[]>([]);
    const [selectedActivity, setSelectedActivity] = useState<IActivity | null>(null);
    const [isEditMode, setEditMode] = useState(false);

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
        const newId: string = await activityService.create(activity);
        const newActivity: IActivity = { ...activity, id: newId };
        setActivities([...activities, newActivity]);
        selectActivity(newActivity.id);
    };

    const editActivityHandler = async (activity: IActivity) => {
        await activityService.update(activity);
        setActivities([...activities.filter(a => a.id !== activity.id), activity]);
        selectActivity(activity.id);
        setSelectedActivity(activity);
    };

    const deleteActivityHandler = async (id: string) => {
        await activityService.delete(id);
        setActivities([...activities.filter(a => a.id !== id)]);
    };

    useEffect(() => {
        const fetch = async () => {
            setActivities(await fetchActivies());
        }
        fetch();
    }, []);

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
                />
            </Container>
        </React.Fragment>
    );
};

export default App;