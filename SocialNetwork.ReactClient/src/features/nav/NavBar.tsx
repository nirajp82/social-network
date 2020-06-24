import React, { useContext, Fragment } from 'react';
import { Container, Menu, Button, Dropdown, Image } from 'semantic-ui-react';
import { NavLink, Link } from 'react-router-dom';

import * as constants from '../../utils/constants';
import { rootStoreContext } from '../../stores/rootStore';

const NavBar: React.FC = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const userStore = rootStoreObj.userStore;

    return (
        <Menu fixed="top" inverted>
            <Container>
                <Menu.Item header as={NavLink} to={`${constants.NAV_HOME}`} exact >
                    <img src="/assets/logo.png" alt="logo" />
                    Social Network
                </Menu.Item>
                {userStore.isUserLoggedIn && userStore.user &&
                    (<Fragment>
                        <Menu.Item name='Activities' as={NavLink} to={`${constants.NAV_ACTIVITIES}`} />

                        <Menu.Item>
                            <Button as={NavLink} to={`${constants.NAV_CREATE_ACTIVITY}`}
                                content="Create Activity" positive />
                        </Menu.Item>

                        <Menu.Item position='right'>
                            <Image avatar spaced='right' src={userStore.user?.image || '/assets/user.png'} />
                            <Dropdown pointing='top left' text={userStore.user?.displayName}>
                                <Dropdown.Menu>
                                    <Dropdown.Item
                                        as={Link}
                                        to={`/profile/${userStore.user?.appUserId}`}
                                        text='My profile'
                                        icon='user'
                                    />
                                    <Dropdown.Item onClick={() => userStore.logout()} text='Logout' icon='power' />
                                </Dropdown.Menu>
                            </Dropdown>
                        </Menu.Item>
                    </Fragment>)}
            </Container>
        </Menu>
    )
};

export default NavBar;