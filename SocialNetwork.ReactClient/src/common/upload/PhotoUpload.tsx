import React, { useState, useEffect, Fragment } from 'react';
import { Grid, Header, Button } from 'semantic-ui-react';

import PhotoDropzone from './PhotoDropzone';
import PhotoCropper from './PhotoCropper';

interface IProps {
    uploadPhoto: (image: Blob) => Promise<any>;
};

const PhotoUpload: React.FC<IProps> = ({ uploadPhoto }) => {
    const [files, setFiles] = useState<any[]>([]);
    const [image, setImage] = useState<Blob | null>(null);
    const [isUploading, setIsUploading] = useState(false);

    const onUploadClick = async () => {
        setIsUploading(true);
        await uploadPhoto(image!);
        setIsUploading(false);
    };

    useEffect(() => {
        //Cleanup Resource: ObjectURL was created for preview when file was uploaded, 
        //to avoid memory leak, releases an object URL
        return () => {
            files.forEach((file) => {
                URL.revokeObjectURL(file.preview);
            });
        };
    }, [files]);

    return (
        <Grid>
            <Grid.Column width={4}>
                <Header sub color='teal' content='Step 1 - Add Photo' />
                <PhotoDropzone setFiles={setFiles} />
            </Grid.Column>
            <Grid.Column width={1}>
            </Grid.Column>
            <Grid.Column width={4}>
                <Header sub color='teal' content='Step 2 - Resize Image' />
                {
                    files && files.length > 0 &&
                    <PhotoCropper setImage={setImage} previewImage={files[0].preview} />
                }
            </Grid.Column>
            <Grid.Column width={1}>
            </Grid.Column >
            <Grid.Column width={4}>
                <Header sub color='teal' content='Step 3 - Preview & Upload' />
                {
                    files && files.length > 0 &&
                    (<Fragment>
                        <div className='img-preview' style={{ minHeight: 200, overflow: 'hidden' }}> </div>
                        <Button.Group widths={2}>
                            <Button loading={isUploading} onClick={() => onUploadClick()} icon='check' positive/>
                            <Button disabled={isUploading} onClick={() => setFiles([])} icon='close'/>
                        </Button.Group>
                    </Fragment>)
                }
            </Grid.Column>
        </Grid>
    );
};

export default PhotoUpload;
