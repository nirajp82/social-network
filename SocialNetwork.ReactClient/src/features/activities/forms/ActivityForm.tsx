import React, { useState, FormEvent, useContext, useEffect } from 'react';
import { Form, Segment, Button } from 'semantic-ui-react';
import { IActivity } from '../../../models/IActivity';
import activityStore from '../../../stores/activityStore';
import { observer } from 'mobx-react-lite';
import { RouteComponentProps, Link } from 'react-router-dom';
import * as constants from '../../../util/constants';


interface IRouteProp {
    id: string;
}

const ActivityForm: React.FC<RouteComponentProps<IRouteProp>> = (props) => {
    const activityStoreObj = useContext(activityStore);
    const { loadActivity } = activityStoreObj;

    let blankActivity: IActivity = {
        id: '',
        title: '',
        description: '',
        date: null,
        category: '',
        city: '',
        venue: ''
    };
    const [activity, setActivity] = useState<IActivity>(blankActivity);

    const handleInpuyChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.currentTarget;
        setActivity({ ...activity, [name]: value });
    };

    const handleSubmitForm = async () => {
        let activityId = "";
        if (activity.id === '')
            activityId = await activityStoreObj.createActivity(activity);
        else {
            await activityStoreObj.editActivity(activity);
            activityId = activity.id;
        }
        props.history.push(`${constants.NAV_ACTIVITY_DETAIL}/${activityId}`);
    }

    useEffect(() => {
        let isComponentMounted = true;
        if (props.match.params.id && props.match.params.id.length > 0) {
            const load = async () => {
                const activity = await loadActivity(props.match.params.id);
                if (isComponentMounted && activity)
                    setActivity(activity);
            }
            load();
        }

        //To fix following warning: 
        //Can't perform a React state update on an unmounted component. This is a no-op, but it indicates a memory leak in application
        return () => {
            isComponentMounted = false;
        };
    }, [loadActivity, props.match.params.id]);

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
                    value={activity.date || ''} />

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
                <Button as={Link} to={constants.NAV_ACTIVITIES} floated="right" type="Button" content="Cancel" />
            </Form>
        </Segment>
    );
};

export default observer(ActivityForm);