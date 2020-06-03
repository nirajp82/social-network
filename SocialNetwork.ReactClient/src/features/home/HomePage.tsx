import React from 'react';
import { Container, Header, Segment, Image, Button } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import * as constants from '../../utils/constants';

const Home: React.FC = () => {
    return (
        <Segment inverted textAlign='center' vertical className='masthead'>
            <Container text>
                <Header as='h1' inverted>
                    <Image size='massive' src='/assets/logo.png' alt='logo' style={{ marginBottom: 12 }} />
                    Social Network
                </Header>

                <Header as='h2' inverted content={`Welcome to Social Network App`} />
                <Button as={Link} to={constants.NAV_ACTIVITIES} size='huge' inverted>
                    Go to activities!
                </Button>
            </Container>
        </Segment>
    );
}

export default Home;