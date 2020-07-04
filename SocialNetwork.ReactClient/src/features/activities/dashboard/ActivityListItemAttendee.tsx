import React from 'react';
import { List, Image, Popup } from 'semantic-ui-react';
import { IAttendee } from '../../../models/IActivity';

interface IProps {
    attendees: IAttendee[]
}

const styles = {
    borderColor: 'orange',
    borderWidth: 2
};

const ActivityListItemAttendee: React.FC<IProps> = ({ attendees }) => {
    return (
        <List horizontal>
            {
                attendees?.map((attendee: IAttendee) => {
                    let path = attendee.image || '/assets/user.png';
                    return (
                        <List.Item key={attendee.appUserId}>
                            <Popup
                                header={attendee.displayName}
                                trigger={
                                    <Image src={path} size='mini'
                                        circular
                                        bordered
                                        style={attendee.following ? styles : null}
                                    />
                                }
                            />
                        </List.Item>
                    );
                })
            }
        </List>
    )
};

export default ActivityListItemAttendee;