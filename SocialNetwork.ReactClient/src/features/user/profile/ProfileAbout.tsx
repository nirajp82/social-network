import React, { useContext, useState } from 'react';
import { Tab, Grid, Button, Header, Card, Icon } from 'semantic-ui-react';

import ProfileEditForm from './forms/ProfileEditForm';
import { rootStoreContext } from '../../../stores/rootStore';

const ProfileAbout = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const { userProfile, isViewingOwnProfile, updateProfile } = rootStoreObj.profileStore;
    const [editMode, setEditMode] = useState(false);

    return (
        <Tab.Pane>
            <Grid>
                <Grid.Column width={16}>
                    <Header
                        floated='left'
                        icon='user'
                        content={`About ${userProfile?.displayName}`}
                    />
                    {isViewingOwnProfile && (
                        <Button
                            floated='right'
                            basic
                            content={editMode ? 'Cancel' : 'Edit Profile'}
                            onClick={() => setEditMode(!editMode)}
                        />
                    )}
                </Grid.Column>
                <Grid.Column width={16}>
                    {editMode ? (
                        <ProfileEditForm
                            setEditMode={setEditMode}
                            updateProfile={updateProfile}
                            userProfile={userProfile!} />
                    ) : (
                            <Card fluid>
                                <Card.Content>
                                    <Card.Description>
                                        {userProfile?.bio}
                                    </Card.Description>
                                </Card.Content>
                                <Card.Content extra>
                                    <Icon name='mail' />
                                    {userProfile?.email}
                                </Card.Content>
                            </Card>
                        )}
                </Grid.Column>
            </Grid>
        </Tab.Pane>
    );
};

export default ProfileAbout;

//<Container text>
//    <Segment.Group>
//        <Segment>First Name: {.firstName}</Segment>
//        <Segment>Last Name: {userProfile?.lastName}</Segment>
//        <Segment>Email: {userProfile?.email}</Segment>
//        <Segment>Bio: {userProfile?.bio}</Segment>
//    </Segment.Group>
//</Container>