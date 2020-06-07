import React from 'react';
import { Item, Button, Segment, Icon } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import moment from 'moment';

import { IActivity } from '../../../models/IActivity';
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
                Name of Sundarsath
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