import React, { Fragment } from 'react';
import { Item, Button, Segment, Icon, Image } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import moment from 'moment';

import { IActivity, IAttendee } from '../../../models/IActivity';
import * as constants from '../../../utils/constants';

interface IProps {
    activity: IActivity;
};

const ActivityListItem: React.FC<IProps> = ({ activity }) => {
    const getAttendees = (activity: IActivity) => {
        return activity.attendees?.map((attendee: IAttendee) => {
            let path = attendee.image || '/assets/user.png';
            return (
                <Fragment key={attendee.appUserId}>
                    <Image src={path} size='mini' centered circular verticalAlign='bottom' />
                    <span>{attendee.displayName}</span>
                </Fragment>
            );
        });
    };

    return (
        <Segment.Group>
            <Segment>
                <Item.Group>
                    <Item>
                        <Item.Image size="tiny" circular src='/assets/user.png' />
                    </Item>
                    <Item.Content>
                        <Item.Header as='a'>{activity.title}</Item.Header>
                        <Item.Description>
                            Hosted By Almighty Shree Raj
                        </Item.Description>
                    </Item.Content>
                </Item.Group>
            </Segment>
            <Segment>
                <Icon name="clock" />{moment(activity.date).format("h:mm A")}
                <Icon name="marker" />{activity.venue}, {activity.city}
            </Segment>
            <Segment secondary>
                {getAttendees(activity)}
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