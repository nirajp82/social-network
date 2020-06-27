import React, { useCallback } from 'react';
import { useDropzone } from 'react-dropzone';
import { Icon, Header } from 'semantic-ui-react';

const dropzoneStyles = {
    border: 'dashed 3px',
    borderColor: '#eee',
    borderRadius: '5px',
    paddingTop: '30px',
    textAlign: 'center' as 'center',
    height: '200px'
};

const dropzoneActive = {
    borderColor: 'teal'
};

interface IProps {
    setFiles: (files: object[]) => void;
}

const PhotoDropzone: React.FC<IProps> = ({ setFiles }) => {
    const onDrop = useCallback(acceptedFiles => {
        const files: object[] = acceptedFiles.map((file: object) => {
            console.log(typeof file);
            return {
                file: file,
                preview: URL.createObjectURL(file)
            };
        });
        console.log(files);
        setFiles(files);
    }, [setFiles]);

    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop });

    return (
        <div {...getRootProps()}
            style={isDragActive ? { ...dropzoneStyles, ...dropzoneActive } : dropzoneStyles}>
            <input {...getInputProps()} />
            <Icon name='upload' size='huge' />
            <Header content='Drop image here' />
        </div>
    );
};

export default PhotoDropzone;
