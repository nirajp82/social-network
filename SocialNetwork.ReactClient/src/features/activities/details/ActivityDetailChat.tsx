import React, { Fragment } from 'react';
import { Segment, Header, Comment, Button, Form } from 'semantic-ui-react';

const ActivityDetailChat: React.FC = () => {
    return (
        <Fragment>
            <Segment
                textAlign='center'
                attached='top'
                inverted
                color='teal'
                style={{ border: 'none' }}>
                <Header>Chat about this event</Header>
            </Segment>
            <Segment attached>
                <Comment.Group>
                    <Comment>
                        <Comment.Avatar src={'/assets/user.png'} />
                        <Comment.Content>
                            <Comment.Author>Shree Raj</Comment.Author>
                            <Comment.Metadata>
                                <div>{new Date().toDateString()}</div>
                            </Comment.Metadata>
                            <Comment.Text>Shree Raj Jay Raj Jay Jay Raj</Comment.Text>
                        </Comment.Content>
                    </Comment>
                    <Form reply>
                        <Form.TextArea />

                        <Button content='Add Reply' labelPosition='left' icon='edit' primary />
                    </Form>
                </Comment.Group>
            </Segment>
        </Fragment>
    );
};

export default ActivityDetailChat;