import React, { Fragment, useContext } from 'react';
import { Menu, Header } from 'semantic-ui-react';
import { Calendar } from 'react-widgets';

import { rootStoreContext } from '../../../stores/rootStore';
import * as constants from '../../../utils/constants';
import { observer } from 'mobx-react-lite';

const ActivityFilter = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const { setPredicate, predicate } = rootStoreObj.activityStore;

    return (
        <Fragment>
            <Menu vertical size={'large'} style={{ width: '100%', marginTop: 50 }}>
                <Header icon={'filter'} attached color={'teal'} content={'Filters'} />
                <Menu.Item
                    active={!predicate.has(constants.PREDICATE_IS_GOING) && !predicate.has(constants.PREDICATE_IS_HOST)}
                    onClick={() => setPredicate(constants.PREDICATE_ALL, 'true')}
                    color={'blue'}
                    name={'all'}
                    content={'All Activities'} />

                <Menu.Item
                    active={predicate.has(constants.PREDICATE_IS_GOING)}
                    onClick={() => setPredicate(constants.PREDICATE_IS_GOING, 'true')}
                    color={'blue'}
                    name={'username'}
                    content={"I'm Going"} />

                <Menu.Item
                    active={predicate.has(constants.PREDICATE_IS_HOST)}
                    onClick={() => setPredicate(constants.PREDICATE_IS_HOST, 'true')}
                    color={'blue'}
                    name={'host'}
                    content={"I'm hosting"}
                />
            </Menu>
            <Header icon={'calendar'} attached color={'teal'} content={'Select Date'} />
            <Calendar
                onChange={(date) => setPredicate(constants.PREDICATE_START_DATE, date!)}
                value={predicate.get(constants.PREDICATE_START_DATE) || new Date()}
            />
        </Fragment>
    );
};

export default observer(ActivityFilter);