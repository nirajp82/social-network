import React from 'react';
import { IProfile } from '../../../models/IProfile';
import { Tab } from 'semantic-ui-react';
import ProfilePhoto from './ProfilePhoto';

interface IProps {
    profile: IProfile
};

const panes = (profile: IProfile) => [
    { menuItem: 'About', render: () => <Tab.Pane>About</Tab.Pane> },
    { menuItem: 'Photos', render: () => <ProfilePhoto /> },
    { menuItem: 'Activities', render: () => <Tab.Pane>Activities</Tab.Pane> },
    { menuItem: 'Followers', render: () => <Tab.Pane>Followers</Tab.Pane> },
    { menuItem: 'Following', render: () => <Tab.Pane>Following</Tab.Pane> },
];

const ProfileContent: React.FC<IProps> = ({ profile }) => {
    return (
        <Tab menu={{ fluid: true, vertical: true }}
            menuPosition='right'
            activeIndex='1'
            panes={panes(profile)} />
    );
};

export default ProfileContent;