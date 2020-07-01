import React from 'react';
import { Loader } from 'semantic-ui-react';

interface IProps {
    message: string;
}
const Spinner: React.FC<IProps> = ({ message }) => {
    return (
        <Loader active inline='centered'>{message}</Loader>
    )
};

export default Spinner;
