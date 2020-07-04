import React from 'react';
import { Card, Image, Icon } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { IProfile } from '../../../models/IProfile';
import * as constants from '../../../utils/constants';

interface IProps {
    profile: IProfile;
    isFollowersTab: boolean;
}

const ProfileCard: React.FC<IProps> = ({ profile, isFollowersTab }) => {
    return (
        <Card as={Link} to={`${constants.NAV_USER_PROFILE}/${profile.appUserId}`}>
            <Image src={profile.mainPhoto?.url || '/assets/user.png'} />
            <Card.Content>
                <Card.Header>{profile.displayName}</Card.Header>
            </Card.Content>
            <Card.Content extra>
                <div>
                    <Icon name='user' />
                    {isFollowersTab ? `${profile.followersCount}` : `${profile.followingCount}`}
                </div>
            </Card.Content>
        </Card>
    )
};

export default observer(ProfileCard);