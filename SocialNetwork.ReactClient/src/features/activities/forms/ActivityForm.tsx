import React, { useState, FormEvent, useContext } from 'react';
import { Form, Segment, Button } from 'semantic-ui-react';
import { IActivity } from '../../../models/IActivity';
import activityStore from '../../../stores/activityStore';
import { observer } from 'mobx-react-lite';

const ActivityForm: React.FC = () => {
    const activityStoreObj = useContext(activityStore);
    const initializeForms = (): IActivity => {
        let value: IActivity;
        if (activityStoreObj.selectedActivity) {
            value = activityStoreObj.selectedActivity;
        }
        else {
            value = {
                id: '',
                title: '',
                description: '',
                date: new Date(),
                category: '',
                city: '',
                venue: ''
            };
        }
        return value;
    };

    const [activity, setActivity] = useState<IActivity>(initializeForms);

    const handleInpuyChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.currentTarget;
        setActivity({ ...activity, [name]: value });
    };
   
    const handleSubmitForm = async () => {
        if (activity.id === '')
            await activityStoreObj.createActivity(activity);
        else
            await activityStoreObj.editActivity(activity);
    }

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmitForm}>
                <Form.Input
                    onChange={handleInpuyChange}
                    name="title"
                    placeholder="Title"
                    value={activity.title}
                />

                <Form.TextArea
                    name="description"
                    onChange={handleInpuyChange}
                    rows={2}
                    placeholder="Description"
                    value={activity.description} />

                <Form.Input
                    name="category"
                    onChange={handleInpuyChange}
                    placeholder="Category"
                    value={activity.category} />

                <Form.Input
                    name="date"
                    onChange={handleInpuyChange}
                    placeholder="Date"
                    value={activity.date} />

                <Form.Input
                    name="city"
                    onChange={handleInpuyChange}
                    placeholder="City"
                    value={activity.city} />

                <Form.Input
                    name="venue"
                    onChange={handleInpuyChange}
                    placeholder="Venue"
                    value={activity.venue} />

                <Button floated="right" type="Submit" loading={activityStoreObj.isSaving} positive content="Submit" />
                <Button onClick={() => activityStoreObj.setShowFormFlag(false)} floated="right" type="Button" content="Cancel" />
            </Form>
        </Segment>
    );
};

export default observer(ActivityForm);