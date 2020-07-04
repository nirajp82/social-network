import React, { Fragment } from 'react';
import { Segment, List, Image, Item, Label } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { IAttendee } from '../../../models/IActivity';
import { NAV_USER_PROFILE } from '../../../utils/constants';
import { observer } from 'mobx-react-lite';

interface IProps {
    attendees: IAttendee[]
}

const ActivityDetailedSidebar: React.FC<IProps> = ({ attendees }) => {
    return (
        <Fragment>
            <Segment
                textAlign='center'
                style={{ border: 'none' }}
                attached='top'
                secondary
                inverted
                color='teal'>
                {`${attendees?.length} ${attendees?.length > 1 ? 'People' : 'Person'} going`}
            </Segment>
            <Segment attached>
                <List relaxed divided>
                    {
                        attendees?.map((attendee: IAttendee) => {
                            return (
                                <Item key={attendee.appUserId} style={{ position: 'relative' }}>
                                    {attendee.isHost &&
                                        (<Label
                                            style={{ position: 'absolute' }}
                                            color='orange'
                                            ribbon='right'>
                                            Host
                                        </Label>)
                                    }
                                    <Image size='tiny' src={attendee.image || '/assets/user.png'} />
                                    <Item.Content verticalAlign='middle'>
                                        <Item.Header as='h3'>
                                            <Link to={`${NAV_USER_PROFILE}/${attendee.appUserId}`}>
                                                {attendee.displayName}
                                            </Link>
                                        </Item.Header>
                                        {attendee.following &&
                                            <Item.Extra style={{ color: 'orange' }}>Following</Item.Extra>
                                        }
                                    </Item.Content>
                                </Item>
                            );
                        })
                    }
                </List>
            </Segment>
        </Fragment>
    );
};

export default observer(ActivityDetailedSidebar);