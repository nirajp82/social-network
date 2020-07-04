import React from 'react';
import { Loader } from 'semantic-ui-react';

interface IProps {
    message?: string;
    loading: boolean;
}
const Spinner: React.FC<IProps> = ({ message, loading }) => {
    return (
        <Loader active={loading} inline='centered'>{message}</Loader>
    )
};

export default Spinner;
