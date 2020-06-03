import React from 'react';
import { Container, Menu, Button } from 'semantic-ui-react';
import { NavLink } from 'react-router-dom';
import * as constants from '../../utils/constants';


const NavBar: React.FC = () => {
    return (
        <Menu fixed="top" inverted>
            <Container>
                <Menu.Item header as={NavLink} to={`${constants.NAV_HOME}`} exact >
                    <img src="/assets/logo.png" alt="logo" />
                    Social Network
                </Menu.Item>

                <Menu.Item name='Activities' as={NavLink} to={`${constants.NAV_ACTIVITIES}`} exact />

                <Menu.Item>
                    <Button as={NavLink} to={`${constants.NAV_CREATE_ACTIVITY}`}  exact
                        content="Create Activity" positive />
                </Menu.Item>                
            </Container>
        </Menu>
    )
};

export default NavBar;