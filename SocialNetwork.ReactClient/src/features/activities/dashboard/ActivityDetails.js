import React from 'react';
import { Icon, Card, Image, ButtonGroup, Button } from 'semantic-ui-react';

const ActivityDetails = () => {
    return (
        <Card fluid>
            <Image src='/assets/placeholder.png' wrapped ui={false} />
            <Card.Content>
                <Card.Header>Matthew</Card.Header>
                <Card.Meta>
                    <span>Joined in 2015</span>
                </Card.Meta>
                <Card.Description>
                    Matthew is a musician living in Nashville.
                </Card.Description>
            </Card.Content>
            <Card.Content extra>
                <ButtonGroup widths="2">
                    <Button basic color='blue' content='Edit' />
                    <Button basic color='grey' content='cancel' />
                </ButtonGroup>
            </Card.Content>
        </Card>
    )
};

export default ActivityDetails;