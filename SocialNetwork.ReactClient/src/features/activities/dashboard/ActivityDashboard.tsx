import React from 'react';
import { Grid } from 'semantic-ui-react';
import { IActivity } from '../../../models/IActivity';
import ActivityList from './ActivityList';
import AcitivityDetails from './ActivityDetails';

interface IProps {
    activities: IActivity[]
}

const ActivityDashboard: React.FC<IProps> = ({ activities }) => {
    return (
        <Grid>
            <Grid.Column width={10} >
                <ActivityList activities={activities} />
            </Grid.Column>
            <Grid.Column width={6} >
                <AcitivityDetails />
            </Grid.Column>
        </Grid>
    );
}

export default ActivityDashboard;