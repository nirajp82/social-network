import React, { useEffect } from 'react';
import { RouteComponentProps } from 'react-router-dom';

interface iRouteProps {
    appUserId: string
};

const UserProfile: React.FC<RouteComponentProps<iRouteProps>> = (props) => {
    const [user, setUser] = React.useState(null);

   // useEffect(
   //, [props.match.params.appUserId]);

    return (
        <div>{props.match.params.appUserId}</div>
    );
};

export default UserProfile;