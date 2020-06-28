import React, { useContext } from 'react';
import { Segment, Item, Header, Button, Grid, Statistic, Divider, Reveal } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { rootStoreContext } from '../../../stores/rootStore';


const ProfileHeader = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const { userProfile } = rootStoreObj.profileStore;

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
                    <Divider />
                    <Reveal animated='move'>
                        <Reveal.Content visible style={{ width: '100%' }}>
                            <Button fluid color='teal' content='Not following' />
                        </Reveal.Content>
                        <Reveal.Content hidden>
                            <Button fluid basic content='Unfollow' />
                        </Reveal.Content>
                    </Reveal>
                </Grid.Column>
            </Grid>
        </Segment>
    );
};

export default observer(ProfileHeader);
