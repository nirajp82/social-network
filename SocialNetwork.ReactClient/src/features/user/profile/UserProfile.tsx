import React, { useEffect, useContext, Fragment } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { IProfile } from '../../../models/IProfile';
import { rootStoreContext } from '../../../stores/rootStore';

import ProfileHeader from './ProfileHeader';
import ProfileContent from './ProfileContent';
import ProgressBar from '../../../layout/ProgressBar';

interface iRouteProps {
    appUserId: string
};

interface iProps extends RouteComponentProps<iRouteProps> {
};

const UserProfile: React.FC<iProps> = (props) => {
    const rootStoreObj = useContext(rootStoreContext);
    const { getUserProfile, isLoadingProfile } = rootStoreObj.profileStore;
    const [profile, setProfile] = React.useState<IProfile | null>(null);

    useEffect(() => {
        const load = async () => {
            const userProfile = await getUserProfile(props.match.params.appUserId);
            setProfile(userProfile!);
        }
        load();
    }, [getUserProfile, props.match.params.appUserId]);

    //Profile Photo
    if (isLoadingProfile)
        return <ProgressBar message="Loading Profile" />

    return (
        <Fragment>
            <ProfileHeader profile={profile} />
            <ProfileContent profile={profile!} />
        </Fragment>
    );
};

export default UserProfile;