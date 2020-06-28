import React, { useState, useContext, useEffect } from 'react';
import { Form, Segment, Button, Grid } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { Form as FinalForm, Field } from 'react-final-form';
import { RouteComponentProps } from 'react-router-dom';
import { combineValidators, isRequired, composeValidators, hasLengthGreaterThan } from 'revalidate';

import { rootStoreContext } from '../../../stores/rootStore';
import { ActivityFormValues } from '../../../models/IActivity';
import * as constants from '../../../utils/constants';
import * as util from '../../../utils/util';
import TextInput from '../../../common/elements/TextInput';
import TextAreaInput from '../../../common/elements/TextAreaInput';
import SelectInput from '../../../common/elements/SelectInput';
import DateInput from '../../../common/elements/DateInput';
import { categoryOptions } from '../../../common/options/categoryOptions';
import createBrowserHistory from '../../../utils/createBrowserHistory';

interface IRouteProp {
    id: string;
}

const validationRules = combineValidators({
    title: isRequired({ message: 'The Event title is required' }),
    description: composeValidators(
        isRequired('Description'),
        hasLengthGreaterThan(4)({ message: 'Description needs to be at least 5 characters' }),
    )(),
    category: isRequired({ message: 'The Event category is required' }),
    date: isRequired('Date'),
    time: isRequired('Time'),
    city: isRequired('City'),
    venue: isRequired('Venue')
});

const ActivityForm: React.FC<RouteComponentProps<IRouteProp>> = (props) => {
    const rootStoreObj = useContext(rootStoreContext);
    const activityStoreObj = rootStoreObj.activityStore;
    const { loadActivity } = activityStoreObj;
    const [activity, setActivity] = useState(new ActivityFormValues());

    useEffect(() => {
        if (props.match.params.id) {
            loadActivity(props.match.params.id)
                .then((activity) => {
                    setActivity(new ActivityFormValues(activity));
                })
        }
    }, [loadActivity, props.match.params.id]);

    const redirectToDetailPage = (id: string) => {
        createBrowserHistory.push(`${constants.NAV_ACTIVITY_DETAIL}/${id}`);
    }

    const onCancelClickHandler = () => {
        if (activity.id)
            redirectToDetailPage(activity.id);
        else
            createBrowserHistory.push(constants.NAV_ACTIVITIES);
    }

    const onFinalFormSubmit = async (values: any) => {
        const { date, time, ...activity } = values;
        activity.date = util.combineDateAndTime(values.date!, values.time!);
        if (activity.id) {
            const isSuccess = await activityStoreObj.editActivity(activity);
            if (isSuccess)
                redirectToDetailPage(activity.id);
        }
        else {
            const id = await activityStoreObj.createActivity(activity);
            if (id && id !== '') {
                activity.id = id;
                redirectToDetailPage(id);
            }
        }
    };

    return (
        <Grid>
            <Grid.Column width={10}>
                <Segment clearing>
                    <FinalForm onSubmit={onFinalFormSubmit}
                        validate={validationRules}
                        initialValues={activity}
                        render={(props) => (
                            <Form
                                onSubmit={props.handleSubmit}
                                loading={activityStoreObj.isLoadingActivity}>
                                <Field
                                    name='title'
                                    placeholder='Title'
                                    value={activity.title}
                                    component={TextInput}
                                />

                                <Field
                                    name="description"
                                    placeholder="Description"
                                    rows={3}
                                    value={activity.description}
                                    component={TextAreaInput}
                                />

                                <Field
                                    name="category"
                                    placeholder="Category"
                                    value={activity.category}
                                    component={SelectInput}
                                    options={categoryOptions}
                                />

                                <Form.Group widths='equal'>
                                    <Field component={DateInput}
                                        name="date"
                                        placeholder="Date"
                                        date={true}
                                        value={activity.date}
                                    />

                                    <Field component={DateInput}
                                        name="time"
                                        placeholder="Time"
                                        time={true}
                                        value={activity.time}
                                    />
                                </Form.Group>

                                <Field
                                    name="city"
                                    placeholder="City"
                                    value={activity.city}
                                    component={TextInput}
                                />

                                <Field
                                    name="venue"
                                    placeholder="Venue"
                                    value={activity.venue}
                                    component={TextInput}
                                />

                                <Button
                                    floated="right"
                                    type="Submit"
                                    loading={activityStoreObj.isSaving}
                                    disabled={activityStoreObj.isLoadingActivity || props.invalid || props.pristine}
                                    positive content="Submit" />

                                <Button
                                    onClick={onCancelClickHandler}
                                    floated="right"
                                    type="Button"
                                    disabled={activityStoreObj.isLoadingActivity}
                                    content="Cancel" />
                            </Form>
                        )}>
                    </FinalForm>
                </Segment>
            </Grid.Column>
        </Grid >
    );
};

export default observer(ActivityForm);