import React, { useContext } from 'react';
import { Card, Image, ButtonGroup, Button } from 'semantic-ui-react';
import activityStore from '../../../stores/activityStore';
import { observer } from 'mobx-react-lite';


const ActivityDetails: React.FC = () => {
    const activityStoreObj = useContext(activityStore);
    const selectedActivity = activityStoreObj.selectedActivity;
    return (
        <Card fluid>
            <Image src={`/assets/categoryImages/${selectedActivity?.category}.jpg`} wrapped ui={false} />
            <Card.Content>
                <Card.Header>{selectedActivity?.title}</Card.Header>
                <Card.Meta>
                    <span>{selectedActivity?.date}</span>
                </Card.Meta>
                <Card.Description>
                    {selectedActivity?.description}
                </Card.Description>
                <Card.Description>
                    {selectedActivity?.city}, {selectedActivity?.venue}
                </Card.Description>
            </Card.Content>
            <Card.Content extra>
                <ButtonGroup widths="2">
                    <Button onClick={() => activityStoreObj.setShowFormFlag(true)} basic color='blue' content='Edit' />
                    <Button onClick={() => activityStoreObj.setSelectActivity("")} basic color='grey' content='Cancel' />
                </ButtonGroup>
            </Card.Content>
        </Card>
    )
};

export default observer(ActivityDetails);