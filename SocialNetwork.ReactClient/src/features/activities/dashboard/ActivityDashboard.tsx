import React from 'react';
import { observer } from 'mobx-react-lite';

import { Grid } from 'semantic-ui-react';
import ActivityList from './ActivityList';

const ActivityDashboard: React.FC = () => {

    return (
        <Grid>
            <Grid.Column width={10} >
                <ActivityList />
            </Grid.Column>
        </Grid>
    );
}

export default observer(ActivityDashboard);