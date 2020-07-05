import React, { useContext } from 'react';
import { RouteProps, RouteComponentProps, Route, Redirect } from 'react-router-dom';
import { observer } from 'mobx-react-lite';


import { rootStoreContext } from '../stores/rootStore';
import * as constants from '../utils/constants';

interface IProps extends RouteProps {
    component: React.ComponentType<RouteComponentProps<any>>
}

// A wrapper for <Route> that redirects to the login
// screen if you're not yet authenticated.
const SecureRoute: React.FC<IProps> = ({ component: Component, ...rest }) => {
    const rootStoreObj = useContext(rootStoreContext);
    const { isUserLoggedIn } = rootStoreObj.userStore;
    return (
        <Route
            {...rest}
            render={(props) => isUserLoggedIn ? <Component {...props} /> : <Redirect to={constants.NAV_LOGIN} />}
        />
    );
};


export default observer(SecureRoute);
