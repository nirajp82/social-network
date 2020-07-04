import React, { useEffect, useContext, useState } from 'react';
import { observer } from 'mobx-react-lite';
import { Grid } from 'semantic-ui-react';
import InfiniteScroll from 'react-infinite-scroller';

import ActivityList from './ActivityList';
import { rootStoreContext } from '../../../stores/rootStore';
import ProgressBar from '../../../layout/ProgressBar';
import Spinner from '../../../layout/Spinner';
import ActivityFilter from './ActivityFilter';

const ActivityDashboard: React.FC = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const activityStoreObj = rootStoreObj.activityStore;
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const fetch = async () => {
            setIsLoading(true);
            await activityStoreObj.loadActivities();
            setIsLoading(false);
        }
        fetch();
    }, [activityStoreObj]);

    const loadNextHandler = async () => {
        setIsLoading(true);
        activityStoreObj.setPageNumber(activityStoreObj.currentPageNumber + 1);
        await activityStoreObj.loadActivities();
        setIsLoading(false);
    };

    if (isLoading && activityStoreObj.currentPageNumber === 0)
        return (<ProgressBar message="Loading Activities..." />);

    return (
        <Grid>
            <Grid.Column width={10} >
                <InfiniteScroll
                    pageStart={0}
                    loadMore={loadNextHandler}
                    hasMore={!isLoading && activityStoreObj.currentPageNumber + 1 < activityStoreObj.totalPages}
                    initialLoad={false}
                />
                <ActivityList />
            </Grid.Column>
            <Grid.Column width={6} >
                <ActivityFilter />
            </Grid.Column>
            <Grid.Column width={10} >
                <Spinner loading={isLoading} />
            </Grid.Column>
        </Grid>
    );
}

export default observer(ActivityDashboard);