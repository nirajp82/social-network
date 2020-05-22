import React from 'react';
import { Item, Segment, Button, Label } from 'semantic-ui-react';
import { IActivity } from '../../../models/IActivity';

interface IProps {
    activities: IActivity[]
};

const ActivityList: React.FC<IProps> = ({ activities }) => {
    return (
        <Segment clearing>
            <Item.Group divided>
                {
                    activities.map((item: IActivity) => {
                        return (
                            <Item key={item.id} >
                                <Item.Content>
                                    <Item.Header as='a'>{item.title}</Item.Header>
                                    <Item.Meta>{item.date}</Item.Meta>
                                    <Item.Description>
                                        <div>{item.description}</div>
                                        <div>{item.city} {item.venue}</div>
                                    </Item.Description>
                                    <Item.Extra>
                                        <Button floated="right" content="view" color="blue" />
                                        <Label basic content={item.category} />
                                    </Item.Extra>
                                </Item.Content>
                            </Item>
                        )
                    })
                }
            </Item.Group>);
        </Segment>
    )
};

export default ActivityList;