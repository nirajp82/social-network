import React from 'react';
import { Grid } from 'semantic-ui-react';
import { IActivity } from '../../../models/IActivity';
import ActivityList from './ActivityList';
import ActivityDetails from '../details/ActivityDetails';
import ActivityForm from '../forms/ActivityForm';

interface IProps {
    activities: IActivity[],
    selectedActivity: IActivity | null;
    isEditMode: boolean,
    isSaving: boolean,
    isDeleting: boolean,

    selectActivity: (id: string) => void,
    setEditMode: (value: boolean) => void,

    createActivityHandler: (activity: IActivity) => void,
    editActivityHandler: (activity: IActivity) => void,
    deleteActivityHandler: (id: string) => void
}

const ActivityDashboard: React.FC<IProps> =
    ({ activities, selectedActivity, isEditMode, isSaving, isDeleting, selectActivity,
        setEditMode, createActivityHandler, editActivityHandler, deleteActivityHandler }) => {

        return (
            <Grid>
                <Grid.Column width={10} >
                    <ActivityList
                        activities={activities}
                        selectActivity={selectActivity}
                        deleteActivityHandler={deleteActivityHandler}
                        isDeleting={isDeleting}
                    />
                </Grid.Column>

                <Grid.Column width={6} >
                    {selectedActivity && !isEditMode
                        &&
                        <ActivityDetails
                            key={selectedActivity.id}
                            selectedActivity={selectedActivity}
                            selectActivity={selectActivity}
                            setEditMode={setEditMode} />}

                    {isEditMode &&
                        <ActivityForm
                            key={selectedActivity ? selectedActivity.id : 0}
                            setEditMode={setEditMode}
                            createActivityHandler={createActivityHandler}
                            editActivityHandler={editActivityHandler}
                            selectedActivity={selectedActivity}
                            isSaving={isSaving}
                        />}
                </Grid.Column>
            </Grid>
        );
    }

export default ActivityDashboard;