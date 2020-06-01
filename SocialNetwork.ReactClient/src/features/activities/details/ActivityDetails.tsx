import React, { useContext, useEffect, useState } from 'react';
import { Card, Image, ButtonGroup, Button } from 'semantic-ui-react';
import activityStore from '../../../stores/activityStore';
import { observer } from 'mobx-react-lite';
import { IActivity } from '../../../models/IActivity';
import { RouteComponentProps, Link } from 'react-router-dom';
import ProgressBar from '../../../layout/ProgressBar';
import * as constants from '../../../util/constants';


interface iRouteProps {
    id: string;
};

const ActivityDetails: React.FC<RouteComponentProps<iRouteProps>> = (props) => {
    const activityStoreObj = useContext(activityStore);
    const [activity, setActivity] = useState<IActivity | undefined>(undefined);
    const { loadActivity } = activityStoreObj;


    useEffect(() => {
        let isComponentMounted = true;
        if (props.match.params.id && props.match.params.id.length > 0) {
            const load = async () => {
                const selectedActivity = await loadActivity(props.match.params.id);
                if (isComponentMounted)
                    setActivity(selectedActivity);
            };
            load();
        }

        //To fix following warning: 
        //Can't perform a React state update on an unmounted component. This is a no-op, but it indicates a memory leak in application
        return () => {
            isComponentMounted = false;
        }
    }, [loadActivity, props.match.params.id]);

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
                    <Button as={Link} to={`${constants.NAV_MANAGE_ACTIVITY}/${activity?.id}`} basic color='blue' content='Edit' />
                    <Button onClick={() => props.history.push("/activities")} basic color='grey' content='Cancel' />
                </ButtonGroup>
            </Card.Content>
        </Card>
    )
};

export default observer(ActivityDetails);