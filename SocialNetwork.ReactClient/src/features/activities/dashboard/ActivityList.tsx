import React, { SyntheticEvent, useState } from 'react';
import { Item, Segment, Button, Label } from 'semantic-ui-react';
import { IActivity } from '../../../models/IActivity';

interface IProps {
    activities: IActivity[],
    isDeleting: boolean,
    selectActivity: (id: string) => void
    deleteActivityHandler: (id: string) => void
};

const ActivityList: React.FC<IProps> = ({ activities, isDeleting, selectActivity, deleteActivityHandler }) => {
    const [target, setTarget] = useState('');
    const deleteHander = (event: SyntheticEvent<HTMLButtonElement>, id: string) => {
        setTarget(event.currentTarget.name);
        deleteActivityHandler(id);
    };
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
                                        <Button onClick={() => selectActivity(item.id)}
                                            name={item.id}
                                            content="View"
                                            floated="right"
                                            color="blue" />

                                        <Button
                                            name={item.id}
                                            onClick={(event) => deleteHander(event, item.id)}
                                            loading={target === item.id && isDeleting}
                                            content="Delete"
                                            floated="right"
                                            color="red" />

                                        <Label basic content={item.category} />
                                    </Item.Extra>
                                </Item.Content>
                            </Item>
                        )
                    })
                }
            </Item.Group>
        </Segment>
    )
};

export default ActivityList;