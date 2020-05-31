import React, { useContext, useEffect } from 'react';
import { Container } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { BrowserRouter, Route } from 'react-router-dom';

import ProgressBar from './ProgressBar';
import activityStore from '../stores/activityStore';
import NavBar from '../features/nav/NavBar';
import HomePage from '../features/home/HomePage';
import ActivityDashboard from '../features/activities/dashboard/ActivityDashboard';
import ActivityDetails from '../features/activities/details/ActivityDetails';
import ActivityForm from '../features/activities/forms/ActivityForm';

const App = () => {
    const activityStoreObj = useContext(activityStore);

    useEffect(() => {
        const fetch = async () => {
            await activityStoreObj.loadActivities();
        }
        fetch();
    }, [activityStoreObj]);

    if (activityStoreObj.isLoading) {
        return <ProgressBar message="Loading Activities"></ProgressBar>;
    }
    return (
        <React.Fragment>
            <BrowserRouter>
                <NavBar />
                <Container style={{ marginTop: '7em' }}>
                    <Route path="/" exact component={HomePage} />
                    <Route path="/activities/:id" component={ActivityDetails} />
                    <Route path="/activities" exact component={ActivityDashboard} />
                    <Route path="/createActivity" component={ActivityForm} />
                </Container>
            </BrowserRouter>
        </React.Fragment>
    );
};

export default observer(App);