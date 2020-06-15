import React, { useEffect, useContext } from 'react';
import { Container } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { Route, withRouter, RouteComponentProps, Switch } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';

import NavBar from '../features/nav/NavBar';
import HomePage from '../features/home/HomePage';
import ActivityDashboard from '../features/activities/dashboard/ActivityDashboard';
import ActivityDetails from '../features/activities/details/ActivityDetails';
import ActivityForm from '../features/activities/forms/ActivityForm';
import LoginForm from '../features/user/forms/LoginForm';
import RegisterForm from '../features/user/forms/RegisterForm';
import { rootStoreContext } from '../stores/rootStore';
import * as constants from '../utils/constants';
import NotFound from './NotFound';
import ProgressBar from './ProgressBar';

const App: React.FC<RouteComponentProps> = ({ location }) => {
    const rootStoreObj = useContext(rootStoreContext);
    const commonStore = rootStoreObj.commonStore;
    const { current } = rootStoreObj.userStore;

    useEffect(() => {
        if (commonStore.token) {
            const loadUser = async () => {
                await current().finally(() => commonStore.setAppLoaded());
            };
            loadUser();
        }
        else
            commonStore.setAppLoaded();
    }, [current, commonStore]);

    if (!commonStore.appLoaded) {
        return <ProgressBar message="Loading Applicaation..." />
    }

    return (

        <React.Fragment>
            <ToastContainer position="bottom-right" />

            {/*Root URL.*/}
            <Route path={constants.NAV_HOME} exact component={HomePage} />

            <Route path={'/(.+)'} render={() => (
                <React.Fragment>
                    <NavBar />
                    <Container style={{ marginTop: '7em' }}>
                        <Switch>
                            <Route path={`${constants.NAV_ACTIVITY_DETAIL}/:id`} exact component={ActivityDetails} />
                            <Route path={constants.NAV_ACTIVITIES} exact component={ActivityDashboard} />
                            {/*Key: To fully unmounted and remounted component on ID change.*/}
                            <Route key={location.key} exact
                                path={[constants.NAV_CREATE_ACTIVITY, `${constants.NAV_MANAGE_ACTIVITY}/:id`]}
                                component={ActivityForm} />

                            <Route path={constants.NAV_LOGIN} component={LoginForm} />
                            <Route path={constants.NAV_REGISTER} component={RegisterForm} />

                            <Route component={NotFound} />
                        </Switch>
                    </Container>
                </React.Fragment>
            )} />
        </React.Fragment>
    );
};

export default withRouter(observer(App));