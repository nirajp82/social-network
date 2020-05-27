import React, { useContext, useEffect } from 'react';
import { Container } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';

import ProgressBar from './ProgressBar';
import activityStore from '../stores/activityStore';
import NavBar from '../features/nav/NavBar';
import ActivityDashboard from '../features/activities/dashboard/ActivityDashboard';

const App = () => {
    const activityStoreObj = useContext(activityStore);

    useEffect(() => {
        const fetch = async () => {
            await activityStoreObj.fetchActivities();
        }
        fetch();
    }, [activityStoreObj]);

    if (activityStoreObj.isLoading) {
        return <ProgressBar message="Loading Activities"></ProgressBar>;
    }
    return (
        <React.Fragment>
            <NavBar />
            <Container style={{ marginTop: '7em' }}>
                <ActivityDashboard />
            </Container>
        </React.Fragment>
    );
};

export default observer(App);