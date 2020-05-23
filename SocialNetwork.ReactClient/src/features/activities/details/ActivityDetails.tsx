import React from 'react';
import { Card, Image, ButtonGroup, Button } from 'semantic-ui-react';
import { IActivity } from '../../../models/IActivity';

interface IProps {
    selectedActivity: IActivity,

    selectActivity: (value: string) => void,
    setEditMode: (value: boolean) => void
}

const ActivityDetails: React.FC<IProps> =
    ({ selectedActivity, selectActivity, setEditMode }) => {
        return (
            <Card fluid>
                <Image src={`/assets/categoryImages/${selectedActivity.category}.jpg`} wrapped ui={false} />
                <Card.Content>
                    <Card.Header>{selectedActivity.title}</Card.Header>
                    <Card.Meta>
                        <span>{selectedActivity.date}</span>
                    </Card.Meta>
                    <Card.Description>
                        {selectedActivity.description}
                    </Card.Description>
                    <Card.Description>
                        {selectedActivity.city}, {selectedActivity.venue}
                    </Card.Description>
                </Card.Content>
                <Card.Content extra>
                    <ButtonGroup widths="2">
                        <Button onClick={() => setEditMode(true)} basic color='blue' content='Edit' />
                        <Button onClick={() => selectActivity("")} basic color='grey' content='Cancel' />
                    </ButtonGroup>
                </Card.Content>
            </Card>
        )
    };

export default ActivityDetails;
            //
