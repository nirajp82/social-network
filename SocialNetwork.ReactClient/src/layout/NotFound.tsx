import React from 'react';
import { Segment, Button, Header, Icon } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import * as constants from '../utils/constants';

const NotFound: React.FC = () => {
    return (
        <Segment placeholder>
            <Header icon>
                <Icon name="search" />
                Oops, We've found everywhere but we could not find this.
            </Header>
            <Segment.Inline>
                <Button as={Link} to={constants.NAV_ACTIVITIES} primary >
                    Return to Activity Page
                </Button>
            </Segment.Inline>
        </Segment>
    );
};

export default NotFound;