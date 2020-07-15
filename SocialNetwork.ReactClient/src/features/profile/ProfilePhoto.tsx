import React, { useState, useContext } from 'react';
import { Tab, Header, Card, Image, Button, Grid } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';

import { rootStoreContext } from '../../stores/rootStore';
import PhotoUpload from '../../common/upload/PhotoUpload';

const ProfilePhoto = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const { userProfile, isViewingOwnProfile, uploadPhoto, setMainPhoto, deletePhoto } = rootStoreObj.profileStore;
    const [addPhotoMode, setAddPhotoMode] = useState(false);
    const [isSettingMainPhoto, setIsSettingMainPhoto] = useState(false);
    const [isDeletingPhoto, setIsDeletingPhoto] = useState(false);
    const [target, setTarget] = useState('');

    //useEffect(() => {
    //    setAddPhotoMode(profileStoreObj.isViewingOwnProfile);
    //}, [profileStoreObj.isViewingOwnProfile]);

    const onAddPhoto = () => {
        setAddPhotoMode(!addPhotoMode);
    };

    const onPhotoUploadHandler = async (photo: Blob) => {
        await uploadPhoto(photo);
        setAddPhotoMode(false);
    };

    const onSetMainPhotoHandler = async (e: React.SyntheticEvent<HTMLButtonElement>, photoId: string) => {
        setTarget(e.currentTarget.name)
        setIsSettingMainPhoto(true);
        await setMainPhoto(photoId);
        setIsSettingMainPhoto(false);
    };

    const onDeletePhotoHandler = async (e: React.SyntheticEvent<HTMLButtonElement>, photoId: string) => {
        setTarget(e.currentTarget.name)
        setIsDeletingPhoto(true);
        await deletePhoto(photoId);
        setIsDeletingPhoto(false);
    };

    return (
        <Tab.Pane>
            <Grid>
                <Grid.Column width={16} style={{ paddingBottom: 0 }}>
                    <Header floated='left' icon='image' content='Photos' />
                    {
                        isViewingOwnProfile &&
                        <Button onClick={() => onAddPhoto()} floated='right' primary
                            basic content={addPhotoMode ? 'Cancel' : 'Add Photo'} />
                    }
                </Grid.Column>
                <Grid.Column width={16}>
                    {addPhotoMode ?
                        (<div><PhotoUpload uploadPhoto={onPhotoUploadHandler} /></div>) :
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
                                                    <Button
                                                        name={photo.id}
                                                        onClick={(e) => onSetMainPhotoHandler(e, photo.id)}
                                                        loading={isSettingMainPhoto && target === photo.id}
                                                        disabled={!isViewingOwnProfile || userProfile.mainPhoto?.id === photo.id}
                                                        basic positive content='Main' />

                                                    <Button
                                                        name={photo.id}
                                                        onClick={(e) => onDeletePhotoHandler(e, photo.id)}
                                                        loading={isDeletingPhoto && target === photo.id}
                                                        disabled={!isViewingOwnProfile || userProfile.mainPhoto?.id === photo.id}
                                                        basic negative icon='trash' />
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