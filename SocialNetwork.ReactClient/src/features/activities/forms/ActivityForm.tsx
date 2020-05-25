import React, { useState, FormEvent } from 'react';
import { Form, Segment, Button } from 'semantic-ui-react';
import { IActivity } from '../../../models/IActivity';

interface IProps {
    selectedActivity: IActivity | null,
    isSaving: boolean,

    setEditMode: (value: boolean) => void,
    createActivityHandler: (activity: IActivity) => void
    editActivityHandler: (activity: IActivity) => void
}


const ActivityForm: React.FC<IProps> = ({ selectedActivity, isSaving, setEditMode, createActivityHandler, editActivityHandler }) => {
    const initializeForms = (): IActivity => {
        let value: IActivity;
        if (selectedActivity) {
            value = selectedActivity;
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

    const handleSubmitForm = () => {
        if (activity.id === '') {
            createActivityHandler(activity);
        }
        else {
            editActivityHandler(activity);
        }
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

                <Button floated="right" type="Submit" loading={isSaving} positive content="Submit" />
                <Button onClick={() => setEditMode(false)} floated="right" type="Button" content="Cancel" />
            </Form>
        </Segment>
    );
};

export default ActivityForm;