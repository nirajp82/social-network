﻿import React from 'react';
import { Tab } from 'semantic-ui-react';
import ProfilePhoto from './ProfilePhoto';
import { observer } from 'mobx-react-lite';

const panes = () => [
    { menuItem: 'About', render: () => <Tab.Pane>About</Tab.Pane> },
    { menuItem: 'Photos', render: () => <ProfilePhoto /> },
    { menuItem: 'Activities', render: () => <Tab.Pane>Activities</Tab.Pane> },
    { menuItem: 'Followers', render: () => <Tab.Pane>Followers</Tab.Pane> },
    { menuItem: 'Following', render: () => <Tab.Pane>Following</Tab.Pane> },
];

const ProfileContent = () => {
    return (
        <Tab menu={{ fluid: true, vertical: true }}
            menuPosition='right'
            activeIndex='1'
            panes={panes()} />
    );
};

export default observer(ProfileContent);