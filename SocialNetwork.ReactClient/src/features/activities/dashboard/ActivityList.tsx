import React, { useContext, Fragment } from 'react';
import { Item,  Label } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';

import activityStore from '../../../stores/activityStore';
import ActivityListItem from './ActivityListItem';


const ActivityList: React.FC = () => {
    const activityStoreObj = useContext(activityStore);

    return (
        <Fragment>
            {
                activityStoreObj.activityByDate.map(([group, activities]) => {
                    return (
                        <Fragment key="group">
                            <Label size="large" color="blue">
                                {group}
                            </Label>
                            <Item.Group divided>
                                {
                                    activities.map((item) => {
                                        return <ActivityListItem key={item.id} activity={item} />
                                    })
                                }
                            </Item.Group>
                        </Fragment>
                    )
                })
            }
        </Fragment>
    )
};

export default observer(ActivityList);