import React from 'react';
import { AxiosResponse } from 'axios';
import { Message } from 'semantic-ui-react';

interface IProps {
    error: AxiosResponse,
    text?: string
};

const ErrorMessage: React.FC<IProps> = (props) => {
    return (
        <Message negative>
            <Message.Header>{props.error}</Message.Header>
            {props.text && (<Message.Content>{props.text}</Message.Content>)}
        </Message>
    );
}

export default ErrorMessage;