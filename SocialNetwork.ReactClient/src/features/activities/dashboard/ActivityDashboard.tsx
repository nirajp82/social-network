import React, { useEffect, useContext } from 'react';
import { observer } from 'mobx-react-lite';
import { Grid } from 'semantic-ui-react';
import InfiniteScroll from 'react-infinite-scroller';

import ActivityList from './ActivityList';
import { rootStoreContext } from '../../../stores/rootStore';
import Spinner from '../../../layout/Spinner';
import ActivityFilter from './ActivityFilter';
import ActivityListLoader from './ActivityListLoader';

const ActivityDashboard: React.FC = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const { loadActivities, isLoadingActivities, setPageNumber, currentPageNumber, totalPages } = rootStoreObj.activityStore;

    useEffect(() => {
        const fetch = async () => {
            await loadActivities();
        }
        fetch();
    }, [loadActivities]);

    const loadNextHandler = async () => {
        setPageNumber(currentPageNumber + 1);
        await loadActivities();
    };

    return (
        <Grid>
            <Grid.Column width={10} >
                {(isLoadingActivities && currentPageNumber === 0)
                    ? (<ActivityListLoader />) : (
                        <InfiniteScroll
                            pageStart={0}
                            loadMore={loadNextHandler}
                            hasMore={!isLoadingActivities && currentPageNumber + 1 < totalPages}
                            initialLoad={false}>
                            <ActivityList />
                        </InfiniteScroll>
                    )}
            </Grid.Column>
            <Grid.Column width={6} >
                <ActivityFilter />
            </Grid.Column>
            <Grid.Column width={10} >
                <Spinner loading={isLoadingActivities} />
            </Grid.Column>
        </Grid>
    );
}

export default observer(ActivityDashboard);