import React, { Fragment } from 'react';
import { Segment, List, Image, Item, Label } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

const ActivityDetailedSidebar: React.FC = () => {
    return (
        <Fragment>
            <Segment
                textAlign='center'
                style={{ border: 'none' }}
                attached='top'
                secondary
                inverted
                color='teal'>
                10 People  going
          </Segment>
            <Segment attached>
                <List relaxed divided>
                    <Item style={{ position: 'relative' }}>
                        <Label
                            style={{ position: 'absolute' }}
                            color='orange'
                            ribbon='right'>
                            Host
                        </Label>
                        <Image size='tiny' src='/assets/user.png' />
                        <Item.Content verticalAlign='middle'>
                            <Item.Header as='h3'>
                                <Link to={"#"}>
                                    Shyamaji
                                </Link>
                            </Item.Header>
                            <Item.Extra style={{ color: 'orange' }}>Following</Item.Extra>
                        </Item.Content>
                    </Item>
                </List>
            </Segment>
        </Fragment>
    );
};

export default ActivityDetailedSidebar;