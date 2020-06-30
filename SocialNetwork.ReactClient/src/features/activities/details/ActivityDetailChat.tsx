import React, { Fragment, useContext, useEffect } from 'react';
import { Segment, Header, Comment, Button, Form } from 'semantic-ui-react';
import { Form as FinalForm, Field } from 'react-final-form';
import moment from 'moment';

import { rootStoreContext } from '../../../stores/rootStore';
import { IComment } from '../../../models/IActivity';
import { Link } from 'react-router-dom';
import * as constants from '../../../utils/constants';
import TextAreaInput from '../../../common/elements/TextAreaInput';
import { observer } from 'mobx-react-lite';

const ActivityDetailChat: React.FC = () => {
    const rootStoreObject = useContext(rootStoreContext);
    const { createHubConnection, stopHubConnection, addComment, selectedActivity } = rootStoreObject.activityStore;

    useEffect(() => {
        createHubConnection();
        return () => {
            return stopHubConnection();
        };
    }, [createHubConnection, stopHubConnection]);

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
                    {
                        selectedActivity && selectedActivity!.comments &&
                        selectedActivity.comments.map((comment: IComment) => {
                            return (
                                <Comment key={comment.id}>
                                    <Comment.Avatar src={comment.userImage || '/assets/user.png'} />
                                    <Comment.Content>
                                        <Comment.Author as='Link' to={`/${constants.NAV_USER_PROFILE}/${comment?.userId}`}>
                                            {comment.userDisplayName}
                                        </Comment.Author>
                                        <Comment.Metadata>
                                            <div>{moment(comment.createdAt).format('dddd Do MMM')} at {moment(comment.createdAt).format('h:mm A')}  </div>
                                        </Comment.Metadata>
                                        <Comment.Text>{comment.body}</Comment.Text>
                                    </Comment.Content>
                                </Comment>)
                        })
                    }
                    <FinalForm
                        onSubmit={addComment}
                        render={({ handleSubmit, submitting, pristine, form }) => (
                            <Form onSubmit={() => handleSubmit()!.then(() => form.reset())}>
                                <Field
                                    name='Body'
                                    component={TextAreaInput}
                                    rows={2}
                                    placeholder='Add your comment'
                                />

                                <Button
                                    content='Add Reply'
                                    labelPosition='left'
                                    icon='edit'
                                    primary
                                    loading={submitting}
                                    disabled={pristine}
                                />
                            </Form>
                        )}
                    />

                </Comment.Group>
            </Segment>
        </Fragment>
    );
};

export default observer(ActivityDetailChat);