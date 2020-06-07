import React, { useEffect, useContext } from 'react';
import { observer } from 'mobx-react-lite';
import { Grid } from 'semantic-ui-react';

import ActivityList from './ActivityList';
import activityStore from '../../../stores/activityStore';

const ActivityDashboard: React.FC = () => {
    const activityStoreObj = useContext(activityStore);

    useEffect(() => {
        const fetch = async () => {
            await activityStoreObj.loadActivities();
        }
        fetch();
    }, [activityStoreObj]);

    return (
        <Grid>
            <Grid.Column width={10} >
                <ActivityList />
            </Grid.Column>
        </Grid>
    );
}

export default observer(ActivityDashboard);