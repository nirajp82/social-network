import React, { useContext, useEffect } from 'react';
import { Grid } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';

import { rootStoreContext } from '../../../stores/rootStore';
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
    const rootStoreObj = useContext(rootStoreContext);
    const activityStoreObj = rootStoreObj.activityStore;
    const { loadActivity, selectedActivity } = activityStoreObj;

    useEffect(() => {
        if (props.match.params.id && props.match.params.id.length > 0) {
            const load = async () => {
                await loadActivity(props.match.params.id);
            };
            load();
        }
    }, [loadActivity, props.match.params.id]);

    if (activityStoreObj.isLoadingActivity)
        return <ProgressBar message="Loading Activity" />

    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityDetailHeader activity={selectedActivity!} />
                <ActivityDetailInfo activity={selectedActivity!} />
                <ActivityDetailChat />
            </Grid.Column>
            <Grid.Column width={6}>
                <ActivityDetailSidebar attendees={selectedActivity?.attendees!} />
            </Grid.Column>
        </Grid>
    )
};

export default observer(ActivityDetails);