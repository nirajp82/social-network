import React, { useState, useContext, useEffect } from 'react';
import { Form, Segment, Button, Grid } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { Form as FinalForm, Field } from 'react-final-form';
import { RouteComponentProps, Link } from 'react-router-dom';

import activityStore from '../../../stores/activityStore';
import { ActivityFormValues } from '../../../models/IActivity';
import * as constants from '../../../utils/constants';
import * as util from '../../../utils/util';
import TextInput from '../../../common/form/TextInput';
import TextAreaInput from '../../../common/form/TextAreaInput';
import SelectInput from '../../../common/form/SelectInput';
import DateInput from '../../../common/form/DateInput';
import { categoryOptions } from '../../../common/options/categoryOptions';
import ProgressBar from '../../../layout/ProgressBar';

interface IRouteProp {
    id: string;
}

const ActivityForm: React.FC<RouteComponentProps<IRouteProp>> = (props) => {
    const activityStoreObj = useContext(activityStore);
    const { loadActivity } = activityStoreObj;
    const [activity, setActivity] = useState(new ActivityFormValues());

    //const handleInpuyChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    //    const { name, value } = event.currentTarget;
    //    setActivity({ ...activity, [name]: value });
    //};

    //const handleSubmitForm = async () => {
    //    let activityId = "";
    //    if (activity.id === '')
    //        activityId = await activityStoreObj.createActivity(activity);
    //    else {
    //        await activityStoreObj.editActivity(activity);
    //        activityId = activity.id;
    //    }
    //    props.history.push(`${constants.NAV_ACTIVITY_DETAIL}/${activityId}`);
    //}

    useEffect(() => {
        if (props.match.params.id) {
            loadActivity(props.match.params.id)
                .then(activity => {
                    setActivity(new ActivityFormValues(activity));
                    console.log(activity);
                })
        }
    }, [loadActivity, props.match.params.id]);

    const onFinalFormSubmit = (values: any) => {
        const { date, time, ...activity } = values;
        activity.date = util.combineDateAndTime(values.date!, values.time!);
        console.log(activity);
    };

    if (activityStoreObj.isLoadingActivity)
        return <ProgressBar message="Loading Activity..."></ProgressBar>;

    return (
        <Grid>
            <Grid.Column width={10}>
                <Segment clearing>
                    <FinalForm onSubmit={onFinalFormSubmit}
                        initialValues={activity}
                        render={(props) => (
                            <Form onSubmit={props.handleSubmit}>
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

                                <Button floated="right" type="Submit" loading={activityStoreObj.isSaving} positive content="Submit" />
                                <Button as={Link} to={constants.NAV_ACTIVITIES} floated="right" type="Button" content="Cancel" />
                            </Form>
                        )}>
                    </FinalForm>
                </Segment>
            </Grid.Column>
        </Grid >
    );
};

export default observer(ActivityForm);

