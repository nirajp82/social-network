import React, { useContext, useEffect } from 'react';
import { Container } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { Route, withRouter, RouteComponentProps } from 'react-router-dom';

import ProgressBar from './ProgressBar';
import activityStore from '../stores/activityStore';
import NavBar from '../features/nav/NavBar';
import HomePage from '../features/home/HomePage';
import ActivityDashboard from '../features/activities/dashboard/ActivityDashboard';
import ActivityDetails from '../features/activities/details/ActivityDetails';
import ActivityForm from '../features/activities/forms/ActivityForm';
import * as constants from '../util/constants';

const App: React.FC<RouteComponentProps> = ({ location }) => {
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
            <Route path={constants.NAV_HOME} exact component={HomePage} />

            <Route path={'/(.+)'} render={() => (
                <React.Fragment>
                    <NavBar />
                    <Container style={{ marginTop: '7em' }}>
                        <Route path={`${constants.NAV_ACTIVITY_DETAIL}/:id`} exact component={ActivityDetails} />
                        <Route path={constants.NAV_ACTIVITIES} exact component={ActivityDashboard} />
                        <Route key={location.key} exact
                            path={[constants.NAV_CREATE_ACTIVITY, `${constants.NAV_MANAGE_ACTIVITY}/:id`]}
                            component={ActivityForm} />                        
                    </Container>
                </React.Fragment>
            )} />
        </React.Fragment>
    );
};

export default withRouter(observer(App));