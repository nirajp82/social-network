import React, { useState, useContext, Fragment } from 'react';
import { Tab, Header, Card, Image, Button, Grid } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';

import { rootStoreContext } from '../../../stores/rootStore';

/**
 * Show Add Photo button if viewing logged in user profile
 * Grid top row Add Photo and Photo Icon and below show photos
 * Button Group to delete photo or set as main photo
 */

const ProfilePhoto = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const { userProfile, isUserViewingOwnProfile, } = rootStoreObj.profileStore;
    const [addPhotoMode, setAddPhotoMode] = useState(false);

    //useEffect(() => {
    //    setAddPhotoMode(profileStoreObj.isUserViewingOwnProfile);
    //}, [profileStoreObj.isUserViewingOwnProfile]);

    return (
        <Tab.Pane>
            <Grid>
                <Grid.Column width={16} style={{ paddingBottom: 0 }}>
                    <Header floated='left' icon='image' content='Photos' />
                    {
                        isUserViewingOwnProfile &&
                        <Button floated='right' basic content={addPhotoMode ? 'Cancel' : 'Add Photo'} />
                    }
                </Grid.Column>
                <Grid.Column width={16}>
                    {addPhotoMode ?
                        (<div>Add Photo Widget</div>) :
                        (<Card.Group itemsPerRow={5}>
                            {
                                userProfile && userProfile.photos &&
                                userProfile.photos.map(photo => {
                                    return (
                                        <Card key={photo}>
                                            <Card.Content>
                                                <Image src={photo} />
                                            </Card.Content>
                                            <Card.Content extra style={{ padding: 0 }}>
                                                <Button.Group fluid widths={2}>
                                                    <Button basic positive content='Main' />
                                                    <Button basic negative icon='trash' />
                                                </Button.Group>
                                            </Card.Content>
                                        </Card>
                                    )
                                })
                            }
                        </Card.Group>)}
                </Grid.Column>
            </Grid>
        </Tab.Pane >
    );
};

export default observer(ProfilePhoto);