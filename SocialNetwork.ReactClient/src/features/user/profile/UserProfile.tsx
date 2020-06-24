import React, { useEffect, useContext } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { IProfile } from '../../../models/IProfile';
import { rootStoreContext } from '../../../stores/rootStore';

import ProfileHeader from './ProfileHeader';

interface iRouteProps {
    appUserId: string
};

const UserProfile: React.FC<RouteComponentProps<iRouteProps>> = (props) => {
    const rootStoreObj = useContext(rootStoreContext);
    const { getUserProfile } = rootStoreObj.profileStore;
    const [profile, setProfile] = React.useState<IProfile | null>(null);

    useEffect(() => {
        const load = async () => {
            const userProfile = await getUserProfile(props.match.params.appUserId);
            setProfile(userProfile);
        }
        load();
    }, [getUserProfile, props.match.params.appUserId]);

    return (
        <ProfileHeader profile={profile} />
    );
};

export default UserProfile;