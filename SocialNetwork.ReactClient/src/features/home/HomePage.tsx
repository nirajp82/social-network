import React, { useContext, Fragment } from 'react';
import { Container, Header, Segment, Image, Button } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

import * as constants from '../../utils/constants';
import { rootStoreContext } from '../../stores/rootStore';
import createBrowserHistory from '../../utils/createBrowserHistory';
import { observer } from 'mobx-react-lite';


const Home: React.FC = () => {
    const rootStoreObject = useContext(rootStoreContext);
    const userStoreObj = rootStoreObject.userStore;
    const commonStoreObj = rootStoreObject.commonStore;

    return (
        <Segment inverted textAlign='center' vertical className='masthead'>
            <Container text>
                <Header as='h1' inverted>
                    <Image size='massive' src='/assets/logo.png' alt='logo' style={{ marginBottom: 12 }} />
                    Social Network
                </Header>

                {userStoreObj.isUserLoggedIn === true && commonStoreObj.getToken()
                    ? (<Fragment>
                        <Header as='h2' inverted content={`Welcome back ${userStoreObj.user!.displayName}`} />
                        <Button as={Link} to={constants.NAV_ACTIVITIES} size='huge' inverted>
                            Go to activities!
                        </Button>
                    </Fragment>)
                    : (<Fragment>
                        <Button as={Link} to={constants.NAV_LOGIN} size='huge' inverted>
                            Login
                        </Button>
                        <Button as={Link} to={constants.NAV_REGISTER} size='huge' inverted>
                            Register
                        </Button>
                    </Fragment>)
                }
            </Container>
        </Segment>
    );
}

export default observer(Home);