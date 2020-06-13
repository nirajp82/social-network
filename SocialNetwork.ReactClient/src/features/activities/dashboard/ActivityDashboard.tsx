import React, { useEffect, useContext } from 'react';
import { observer } from 'mobx-react-lite';
import { Grid } from 'semantic-ui-react';

import ActivityList from './ActivityList';
import { rootStoreContext } from '../../../stores/rootStore';

const ActivityDashboard: React.FC = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const activityStoreObj = rootStoreObj.activityStore;

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