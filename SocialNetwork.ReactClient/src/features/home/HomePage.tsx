import React from 'react';
import { Container } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import * as constants from '../../util/constants';

const Home: React.FC = () => {
    return (
        <Container style={{ marginTop: '7em' }}>
            Home Page
            <h3>
                Go to <Link to={constants.NAV_ACTIVITIES}>Activities</Link>
            </h3>
        </Container>
    );
}

export default Home;