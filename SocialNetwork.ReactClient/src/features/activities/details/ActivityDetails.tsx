import React, { useContext, useEffect, useState } from 'react';
import { Grid } from 'semantic-ui-react';
import activityStore from '../../../stores/activityStore';
import { observer } from 'mobx-react-lite';
import { IActivity } from '../../../models/IActivity';
import { RouteComponentProps } from 'react-router-dom';
import ProgressBar from '../../../layout/ProgressBar';
import ActivityDetailChat from './ActivityDetailChat';
import ActivityDetailHeader from './ActivityDetailHeader';
import ActivityDetailInfo from './ActivityDetailInfo';
import ActivityDetailSidebar from './ActivityDetailedSidebar';

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
        //Can't perform a React state update on an unmounted component. 
        //This is a no - op, but it indicates a memory leak in application
        return () => {
            isComponentMounted = false;
        }
    }, [loadActivity, props.match.params.id]);

    if (activityStoreObj.isLoading)
        return <ProgressBar message="Loading Activity" />

    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityDetailHeader activity={activity} />
                <ActivityDetailInfo activity={activity} />
                <ActivityDetailChat />
            </Grid.Column>
            <Grid.Column width={6}>
                <ActivityDetailSidebar />
            </Grid.Column>
        </Grid>
    )
};

export default observer(ActivityDetails);

