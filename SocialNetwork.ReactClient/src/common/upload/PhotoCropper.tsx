import React, { useRef } from 'react';

import Cropper from 'react-cropper';
import 'cropperjs/dist/cropper.css';

interface IProps {
    setImage: (img: Blob) => void,
    previewImage: string
};

const PhotoCropper: React.FC<IProps> = (props) => {
    const cropper = useRef<Cropper>(null);

    const cropImage = () => {
        if (cropper && cropper.current) {

            if (typeof cropper.current.getCroppedCanvas() === 'undefined')
                return;

            cropper.current.getCroppedCanvas().toBlob((blob: any) => {
                props.setImage(blob);
            }, 'image/jpeg');
        }
    };

    return (
        <Cropper
            ref={cropper}
            src={props.previewImage}
            style={{ height: 200, width: '100%' }}
            // Cropper.js options
            aspectRatio={1 / 1}
            preview='.img-preview'
            guides={false}
            viewMode={1}
            dragMode='move'
            scalable={true}
            cropBoxMovable={true}
            cropBoxResizable={true}
            crop={cropImage}
        />
    );
};

export default PhotoCropper;