import React, { SyntheticEvent, useState, useContext } from 'react';
import { Item, Segment, Button, Label } from 'semantic-ui-react';
import { IActivity } from '../../../models/IActivity';
import { observer } from 'mobx-react-lite';
import activityStore from '../../../stores/activityStore';
import activityService from '../../../api/activities';


const ActivityList: React.FC = () => {
    const activityStoreObj = useContext(activityStore);

    const [target, setTarget] = useState('');

    const deleteHander = async (event: SyntheticEvent<HTMLButtonElement>, id: string) => {
        setTarget(event.currentTarget.name);
        await activityStoreObj.deleteActivity(id);
    };

    return (
        <Segment clearing>
            <Item.Group divided>
                {
                    activityStoreObj.activities.map((item: IActivity) => {
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
                                        <Button onClick={() => activityStoreObj.setSelectActivity(item.id)}
                                            name={item.id}
                                            content="View"
                                            floated="right"
                                            color="blue" />

                                        <Button
                                            name={item.id}
                                            onClick={(event) => deleteHander(event, item.id)}
                                            loading={target === item.id && activityStoreObj.isDeleting}
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

export default observer(ActivityList);