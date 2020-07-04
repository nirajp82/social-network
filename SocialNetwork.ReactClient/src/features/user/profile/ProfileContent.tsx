import React, { useContext } from 'react';
import { Tab, TabProps } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';

import ProfilePhoto from './ProfilePhoto';
import ProfileAbout from './ProfileAbout';
import ProfileFollowing from './ProfileFollowing';
import * as constants from '../../../utils/constants';
import { rootStoreContext } from '../../../stores/rootStore';


const panes = () => [
    { menuItem: 'About', render: () => <ProfileAbout /> },
    { menuItem: 'Photos', render: () => <ProfilePhoto /> },
    { menuItem: 'Activities', render: () => <Tab.Pane>Activities</Tab.Pane> },
    { menuItem: 'Followers', render: () => <ProfileFollowing /> },
    { menuItem: 'Following', render: () => <ProfileFollowing /> }
];

const ProfileContent = () => {
    const rootStoreObject = useContext(rootStoreContext);
    return (
        <Tab menu={{ fluid: true, vertical: true }}
            menuPosition='right'
            panes={panes()}
            onTabChange={(_, data: TabProps) => rootStoreObject.profileStore.setActiveTab(data.activeIndex)}
        />
    );
};

export default observer(ProfileContent);