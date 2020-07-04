import React, { Fragment, useContext, useEffect, useState } from 'react';
import { Segment, Header, Comment, Button, Form } from 'semantic-ui-react';
import { Form as FinalForm, Field } from 'react-final-form';
import moment from 'moment';
import { observer } from 'mobx-react-lite';
import { Link } from 'react-router-dom';

import { rootStoreContext } from '../../../stores/rootStore';
import { IComment } from '../../../models/IActivity';
import * as constants from '../../../utils/constants';
import TextAreaInput from '../../../common/elements/TextAreaInput';
import Spinner from '../../../layout/Spinner';


const ActivityDetailChat: React.FC = () => {
    const rootStoreObject = useContext(rootStoreContext);
    const { addComment, selectedActivity, getComments } = rootStoreObject.activityStore;
    const { createHubConnection, stopHubConnection } = rootStoreObject.activityHubStore;
    const [isLoadingComments, setIsLoadingComments] = useState(false);


    useEffect(() => {
        if (selectedActivity) {
            createHubConnection(selectedActivity.id);

            //Stop hub connection on component unmount
            return () => {
                return stopHubConnection(selectedActivity.id);
            };
        }
    }, [createHubConnection, stopHubConnection, selectedActivity]);

    useEffect(() => {
        const load = async () => {
            setIsLoadingComments(true);
            await getComments();
            setIsLoadingComments(false);
        };
        if (selectedActivity)
            load();
    }, [getComments, selectedActivity]);

    if (isLoadingComments)
        return <Spinner message="Loading Comment" loading={true} />

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
                                        <Comment.Author as={Link} to={`${constants.NAV_USER_PROFILE}/${comment?.userId}`}>
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