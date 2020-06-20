import React, { useState, useContext } from 'react';
import { Segment, Image, Item, Header, Button } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { Link } from 'react-router-dom';
import moment from 'moment';

import { IActivity } from '../../../models/IActivity';
import * as constants from '../../../utils/constants';
import { rootStoreContext } from '../../../stores/rootStore';

const activityImageStyle = {
    filter: 'brightness(30%)'
};

const activityImageTextStyle = {
    position: 'absolute',
    bottom: '5%',
    left: '5%',
    width: '100%',
    height: 'auto',
    color: 'white'
};

const ActivityDetailHeader: React.FC<{ activity: IActivity | undefined }> = ({ activity }) => {
    const rootStoreObject = useContext(rootStoreContext);

    const [loading, setLoading] = useState<boolean>(false);

    const attendActivity = async () => {
        setLoading(true);
        try {
            await rootStoreObject.activityStore.attend(activity?.id!);
        } finally {
            setLoading(false);
        }
    };

    const unAttendActivity = async () => {
        setLoading(true);
        try {
            await rootStoreObject.activityStore.unattend(activity?.id!);
        } finally {
            setLoading(false);
        }
    };

    return (
        <Segment.Group>
            <Segment basic attached="top" style={{ padding: '0' }}>
                <Image src={`/assets/categoryImages/${activity?.category}.jpg`}
                    fluid style={activityImageStyle} />
                <Segment basic style={activityImageTextStyle}>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Header size="huge" content={activity?.title} style={{ color: 'white' }}></Header>
                                <p>{moment(activity?.date).format('dddd Do MMM')}</p>
                                <p>Hosted By <strong> Shree Raj</strong> </p>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
            </Segment>
            <Segment clearing attached="bottom">
                {
                    !activity?.isCurrentUserHost && !activity?.isCurrentUserGoing &&
                    <Button loading={loading} onClick={attendActivity} color='teal'>Join Activity</Button>
                }
                {
                    !activity?.isCurrentUserHost && activity?.isCurrentUserGoing &&
                    <Button loading={loading} onClick={unAttendActivity} >Cancel attendence</Button>
                }
                {activity?.isCurrentUserHost &&
                    <Button as={Link} to={`${constants.NAV_MANAGE_ACTIVITY}/${activity?.id}`}
                        color='orange' floated='right'>Manage Event</Button>
                }
            </Segment>
        </Segment.Group>
    );
};

export default observer(ActivityDetailHeader);