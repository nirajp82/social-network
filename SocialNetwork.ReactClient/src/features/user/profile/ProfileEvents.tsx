import React, { useEffect, useContext, useState } from 'react';
import { observer } from 'mobx-react-lite';
import { Tab, Grid, Header, Card, Image, TabProps } from 'semantic-ui-react';

import { Link } from 'react-router-dom';
import moment from 'moment';

import { rootStoreContext } from '../../../stores/rootStore';
import { IUserActivity } from '../../../models/IActivity';
import * as constants from '../../../utils/constants';

const panes = [
    { menuItem: 'Future Events', pane: { key: 'futureEvents' } },
    { menuItem: 'Past Events', pane: { key: 'pastEvents' } },
    { menuItem: 'Hosting', pane: { key: 'hosted' } }
];

const ProfileEvents = () => {
    const [loadingActivities, setLoadingActivities] = useState(false);
    const rootStore = useContext(rootStoreContext);
    const { loadUserActivities, userProfile, userActivities } = rootStore.profileStore;

    useEffect(() => {
        const load = async () => {
            setLoadingActivities(true);
            await loadUserActivities(userProfile!.appUserId, '');
            setLoadingActivities(false);
        };
        load();
    }, [loadUserActivities, userProfile]);

    const handleTabChange = (_: React.MouseEvent<HTMLDivElement, MouseEvent>, data: TabProps) => {
        setLoadingActivities(true);
        let predicate;
        switch (data.activeIndex) {
            case 1:
                predicate = 'past';
                break;
            case 2:
                predicate = 'hosting';
                break;
            default:
                predicate = 'future';
                break;
        }
        loadUserActivities(userProfile!.appUserId, predicate)
            .then(() => { setLoadingActivities(false)});
    };

    return (
        <Tab.Pane loading={loadingActivities}>
            <Grid>
                <Grid.Column width={16}>
                    <Header floated='left' icon='calendar' content={'Activities'} />
                </Grid.Column>
                <Grid.Column width={16}>
                    <Tab
                        panes={panes}
                        menu={{ secondary: true, pointing: true }}
                        onTabChange={(e, data) => handleTabChange(e, data)}
                    />
                    <br />
                    <Card.Group itemsPerRow={4}>
                        {userActivities && userActivities.map((activity: IUserActivity) => (
                            <Card
                                as={Link}
                                to={`${constants.NAV_ACTIVITIES}/${activity.activityId}`}
                                key={activity.activityId}>
                                <Image
                                    src={`/assets/categoryImages/${activity.category}.jpg`}
                                    style={{ minHeight: 100, objectFit: 'cover' }}
                                />
                                <Card.Content>
                                    <Card.Header textAlign='center'>{activity.title}</Card.Header>
                                    <Card.Meta textAlign='center'>
                                        <div>{moment(activity.date).format("MMM Do YYYY")}</div>
                                        <div>{moment(activity.date).format("h:mm A")}</div>
                                    </Card.Meta>
                                </Card.Content>
                            </Card>
                        ))}
                    </Card.Group>
                </Grid.Column>
            </Grid>
        </Tab.Pane>
    );
};

export default observer(ProfileEvents);