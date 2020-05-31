import React, { useContext } from 'react';
import { observer } from 'mobx-react-lite';

import { Grid } from 'semantic-ui-react';
import ActivityList from './ActivityList';
//import ActivityDetails from '../details/ActivityDetails';
//import ActivityForm from '../forms/ActivityForm';
import activityStore from '../../../stores/activityStore';

const ActivityDashboard: React.FC = () => {

    //const activityStoreObj = useContext(activityStore);   

    return (
        <Grid>
            <Grid.Column width={10} >
                <ActivityList />
            </Grid.Column>
        </Grid>
    );
}

export default observer(ActivityDashboard);

//<Grid.Column width={6} >
//    {activityStoreObj.selectedActivity && !activityStoreObj.showForm &&
//        <ActivityDetails key={activityStoreObj.selectedActivity.id} />}

//    {activityStoreObj.showForm &&
//        <ActivityForm key={activityStoreObj.selectedActivity ? activityStoreObj.selectedActivity.id : 0} />}
//</Grid.Column>