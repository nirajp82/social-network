import React from 'react';
import { Segment, Image, Item, Header, Button } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { Link } from 'react-router-dom';

import { IActivity } from '../../../models/IActivity';
import * as constants from '../../../utils/constants';
import moment from 'moment';

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
                                <p>{moment(activity?.date!).format('dddd Do MMM')}</p>
                                <p>Hosted By <strong> Shree Raj</strong> </p>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
            </Segment>
            <Segment clearing attached="bottom">
                <Button color='teal'>Join Activity </Button>
                <Button>Cancel attendence</Button>
                <Button as={Link} to={`${constants.NAV_MANAGE_ACTIVITY}/${activity?.id}`}
                    color='orange' floated='right'>Manage Event</Button>
            </Segment>
        </Segment.Group>
    );
};

export default observer(ActivityDetailHeader);