import React from 'react';
import { Item, Button, Segment, Icon, Label } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import moment from 'moment';

import { IActivity } from '../../../models/IActivity';
import ActivityListItemAttendee from './ActivityListItemAttendee';
import * as constants from '../../../utils/constants';

interface IProps {
    activity: IActivity;
};

const ActivityListItem: React.FC<IProps> = ({ activity }) => {
    return (
        <Segment.Group>
            <Segment>
                <Item.Group>
                    <Item>
                        <Item.Image
                            size='tiny'
                            circular
                            src={activity.host?.image || '/assets/user.png'}
                            style={{ marginBottom: 3 }}
                        />
                        <Item.Content>
                            <Item.Header as={Link} to={`/activities/${activity.id}`}>
                                {activity.title}
                            </Item.Header>
                            <Item.Description>
                                Hosted by
                                <Link to={`/profile/${activity.host?.appUserId}`}> {activity.host?.displayName}</Link>
                            </Item.Description>
                            {activity.isCurrentUserHost && (
                                <Item.Description>
                                    <Label
                                        basic
                                        color='orange'
                                        content='You are hosting this activity'
                                    />
                                </Item.Description>
                            )}
                            {activity.isCurrentUserGoing && !activity.isCurrentUserHost && (
                                <Item.Description>
                                    <Label
                                        basic
                                        color='green'
                                        content='You are going to this activity'
                                    />
                                </Item.Description>
                            )}
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <Icon name="clock" />{moment(activity.date).format("h:mm A")}
                <Icon name="marker" />{activity.venue}, {activity.city}
            </Segment>
            <Segment secondary>
                <ActivityListItemAttendee attendees={activity.attendees} />
            </Segment>
            <Segment clearing>
                <span>{activity.description}</span>
                <Button
                    as={Link}
                    to={`${constants.NAV_ACTIVITIES}/${activity.id}`}
                    name={activity.id}
                    content="View"
                    floated="right"
                    color="blue" />
            </Segment>
        </Segment.Group>

    );
};

export default ActivityListItem;