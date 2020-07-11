import React, { useEffect, useContext, Fragment, useState } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { observer } from 'mobx-react-lite';

import ProfileHeader from './ProfileHeader';
import ProfileContent from './ProfileContent';
import ProgressBar from '../../layout/ProgressBar';
import { rootStoreContext } from '../../stores/rootStore';

interface iRouteProps {
    appUserId: string
};

interface iProps extends RouteComponentProps<iRouteProps> {
};

const UserProfile: React.FC<iProps> = (props) => {
    const rootStoreObj = useContext(rootStoreContext);
    const { getUserProfile } = rootStoreObj.profileStore;
    const [isLoadingProfile, setIsLoadingProfile] = useState(false);

    useEffect(() => {
        const load = async () => {
            setIsLoadingProfile(true);
            await getUserProfile(props.match.params.appUserId);
            setIsLoadingProfile(false);
        }
        load();
    }, [getUserProfile, props.match.params.appUserId]);

    //Profile Photo
    if (isLoadingProfile)
        return <ProgressBar message="Loading Profile" />

    return (
        <Fragment>
            <ProfileHeader />
            <ProfileContent />
        </Fragment>
    );
};

export default observer(UserProfile);