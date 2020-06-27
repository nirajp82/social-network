import React, { useState, useContext } from 'react';
import { Tab, Header, Card, Image, Button, Grid } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';

import { rootStoreContext } from '../../../stores/rootStore';
import PhotoUpload from '../../../common/upload/PhotoUpload';

const ProfilePhoto = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const { userProfile, isUserViewingOwnProfile, uploadPhoto } = rootStoreObj.profileStore;
    const [addPhotoMode, setAddPhotoMode] = useState(true);

    //useEffect(() => {
    //    setAddPhotoMode(profileStoreObj.isUserViewingOwnProfile);
    //}, [profileStoreObj.isUserViewingOwnProfile]);

    const onAddPhoto = () => {
        setAddPhotoMode(!addPhotoMode);
    };

    return (
        <Tab.Pane>
            <Grid>
                <Grid.Column width={16} style={{ paddingBottom: 0 }}>
                    <Header floated='left' icon='image' content='Photos' />
                    {
                        isUserViewingOwnProfile &&
                        <Button onClick={() => onAddPhoto()} floated='right' basic content={addPhotoMode ? 'Cancel' : 'Add Photo'} />
                    }
                </Grid.Column>
                <Grid.Column width={16}>
                    {addPhotoMode ?
                        (<div><PhotoUpload uploadPhoto={uploadPhoto} /></div>) :
                        (<Card.Group itemsPerRow={5}>
                            {
                                userProfile && userProfile.photos &&
                                userProfile.photos.map(photo => {
                                    return (
                                        <Card key={photo.id}>
                                            <Card.Content>
                                                <Image src={photo.url} />
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