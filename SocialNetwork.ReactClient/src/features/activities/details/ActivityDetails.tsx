import React, { useContext, useEffect, useState } from 'react';
import { Card, Image, ButtonGroup, Button } from 'semantic-ui-react';
import activityStore from '../../../stores/activityStore';
import { observer } from 'mobx-react-lite';
import { IActivity } from '../../../models/IActivity';
import { RouteComponentProps } from 'react-router-dom';
import ProgressBar from '../../../layout/ProgressBar';

interface iRouteProps {
    id: string;
};

const ActivityDetails: React.FC<RouteComponentProps<iRouteProps>> = (props) => {
    const activityStoreObj = useContext(activityStore);
    const [activity, setActivity] = useState<IActivity | undefined>(undefined);

    useEffect(() => {
        const load = async () => {
            const selectedActivity = await activityStoreObj.loadActivity(props.match.params.id);
            setActivity(selectedActivity);
        };
        load();
    }, [activityStoreObj.loadActivity]);

    if (activityStoreObj.isLoading) 
        return <ProgressBar message="Loading Activity" />

    return (
        <Card fluid>
            <Image src={`/assets/categoryImages/${activity?.category}.jpg`} wrapped ui={true} />
            <Card.Content>
                <Card.Header>{activity?.title}</Card.Header>
                <Card.Meta>
                    <span>{activity?.date}</span>
                </Card.Meta>
                <Card.Description>
                    {activity?.description}
                </Card.Description>
                <Card.Description>
                    {activity?.city}, {activity?.venue}
                </Card.Description>
            </Card.Content>
            <Card.Content extra>
                <ButtonGroup widths="2">
                    <Button onClick={() => activityStoreObj.setShowFormFlag(true)} basic color='blue' content='Edit' />
                    <Button onClick={() => props.history.push("/activities") } basic color='grey' content='Cancel' />
                </ButtonGroup>
            </Card.Content>
        </Card>
    )
};

export default observer(ActivityDetails);