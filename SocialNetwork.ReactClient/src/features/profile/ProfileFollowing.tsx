import React, { useContext } from 'react';
import { Tab, Grid, Header, Card } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';

import { rootStoreContext } from '../../stores/rootStore';
import ProfileCard from './ProfileCard';

const ProfileFollowing = () => {
    const rootStore = useContext(rootStoreContext);
    const { userProfile, isLoadingfollowers, followers, isUserViewingFollowersTab } = rootStore.profileStore;

    return (
        <Tab.Pane loading={isLoadingfollowers}>
            <Grid>
                <Grid.Column width={16}>
                    <Header
                        floated='left'
                        icon='user'
                        content={
                            isUserViewingFollowersTab
                                ? `${userProfile!.displayName} is followed by below users`
                                : `${userProfile!.displayName} is following below users.`
                        }
                    />
                </Grid.Column>
                <Grid.Column width={16}>
                    <Card.Group itemsPerRow={5}>
                        {
                            followers && followers.length > 0 &&
                            followers?.map((profile) => {
                                return <ProfileCard
                                    key={profile.appUserId}
                                    profile={profile}
                                    isFollowersTab={isUserViewingFollowersTab}
                                />
                            })
                        }
                    </Card.Group>
                </Grid.Column>
            </Grid>
        </Tab.Pane>
    );
}

export default observer(ProfileFollowing);