import React from 'react';
import { Tab } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';

import ProfilePhoto from './ProfilePhoto';
import About from './forms/About';


const panes = () => [
    { menuItem: 'About', render: () => <About /> },
    { menuItem: 'Photos', render: () => <ProfilePhoto /> },
    { menuItem: 'Activities', render: () => <Tab.Pane>Activities</Tab.Pane> },
    { menuItem: 'Followers', render: () => <Tab.Pane>Followers</Tab.Pane> },
    { menuItem: 'Following', render: () => <Tab.Pane>Following</Tab.Pane> },
];

const ProfileContent = () => {
    return (
        <Tab menu={{ fluid: true, vertical: true }}
            menuPosition='right'
            panes={panes()} />
    );
};

export default observer(ProfileContent);