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
            <Message.Header>{props.error.statusText}</Message.Header>

            {props.error.data && Object.keys(props.error.data).length > 0 &&
                (
                    <Message.List>
                        {Object.values(props.error.data.errors).flat().map((err: string, idx: number) => {
                            return (<Message.Item key={idx}>{err}</Message.Item>);
                        })}
                    </Message.List>
                )}

            {props.text && (<Message.Content>{props.text}</Message.Content>)}
        </Message>
    );
}

export default ErrorMessage;