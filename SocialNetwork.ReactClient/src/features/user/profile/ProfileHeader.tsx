import React, { useContext, Fragment, useState } from 'react';
import { Segment, Item, Header, Button, Grid, Statistic, Divider, Reveal } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { rootStoreContext } from '../../../stores/rootStore';

const ProfileHeader = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const [isUpdatingFollowFlag, setIsUpdatingFollowFlag] = useState(false);
    const { userProfile, follow, unfollow, isViewingOwnProfile } = rootStoreObj.profileStore;

    const onFollowClickHandler = async () => {
        setIsUpdatingFollowFlag(true);
        if (userProfile!.following)
            await unfollow(userProfile!.appUserId);
        else
            await follow(userProfile!.appUserId);
        setIsUpdatingFollowFlag(false);
    };

    return (
        <Segment>
            <Grid>
                <Grid.Column width={12}>
                    <Item.Group>
                        <Item>
                            <Item.Image avatar size='small' src={userProfile?.mainPhoto?.url || '/assets/user.png'} />
                            <Item.Content verticalAlign='middle'>
                                <Header as='h1'>{userProfile?.displayName}</Header>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Grid.Column>
                <Grid.Column width={4}>
                    <Statistic.Group widths={2}>
                        <Statistic label='Followers' value={userProfile?.followersCount} />
                        <Statistic label='Following' value={userProfile?.followingCount} />
                    </Statistic.Group>
                    {
                        userProfile && !isViewingOwnProfile ?
                            (
                                <Fragment>
                                    <Divider />
                                    <Reveal animated='move'>
                                        <Fragment>
                                            <Reveal.Content hidden>
                                                <Button
                                                    onClick={() => onFollowClickHandler()}
                                                    content={userProfile!.following ? 'Unfollow' : 'Follow'}
                                                    loading={isUpdatingFollowFlag}
                                                    className={userProfile!.following ? 'negative' : 'positive'}
                                                    fluid basic />
                                            </Reveal.Content>)
                                            <Reveal.Content visible style={{ width: '100%' }}>
                                                <Button
                                                    content={userProfile!.following ? 'Following' : 'Not following'}
                                                    fluid color='teal' />
                                            </Reveal.Content>
                                        </Fragment>
                                    </Reveal>
                                </Fragment>
                            ) : ""
                    }
                </Grid.Column>
            </Grid>
        </Segment>
    );
};

export default observer(ProfileHeader);
