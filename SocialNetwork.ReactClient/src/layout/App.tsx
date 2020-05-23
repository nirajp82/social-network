import React, { useEffect, useState } from 'react';
import Axios from 'axios';

import { IActivity } from '../models/IActivity';
import NavBar from '../features/nav/NavBar';
import ActivityDashboard from '../features/activities/dashboard/ActivityDashboard';
import { Container } from 'semantic-ui-react';


const fetchActivities = async (): Promise<IActivity[]> => {
    const response = await Axios.get<IActivity[]>("http://localhost/socialnetwork/api/Activities",
        {
            withCredentials: true
        });

    return response.data;
};

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

    const createActivityHandler = (activity: IActivity) => {
        activity.id = (activities.length + 1 * -1).toString();
        setActivities([...activities, activity]);
        selectActivity(activity.id);
        //updateEditMode(false);
        //setSelectedActivity(activity);
    };

    const editActivityHandler = (activity: IActivity) => {
        setActivities([...activities.filter(a => a.id !== activity.id), activity]);

        selectActivity(activity.id);
        //updateEditMode(false);
        //setSelectedActivity(activity);
    };

    const deleteActivityHandler = (id: string) => {
        setActivities([...activities.filter(a => a.id !== id)]);
    };

    useEffect(() => {
        const fetch = async () => {
            setActivities(await fetchActivities());
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