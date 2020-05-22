import React, { useEffect, useState } from 'react';
import Axios from 'axios';

import { IActivity } from '../models/IActivity';
import NavBar from '../features/nav/NavBar';
import ActivityDashboard from '../features/activities/dashboard/ActivityDashboard';


const fetchActivities = async (): Promise<IActivity[]> => {
    const response = await Axios.get<IActivity[]>("http://localhost/socialnetwork/api/Activities",
        {
            withCredentials: true
        });
    return response.data;
};

const App = () => {
    const [activities, setActivities] = useState<IActivity[]>([]);

    useEffect(() => {
        const fetch = async () => {
            setActivities(await fetchActivities());
        }
        fetch();
    }, []);

    return (
        <React.Fragment>
            <NavBar />
            <ActivityDashboard activities={activities} />
        </React.Fragment>
    );
};

export default App;