import React from 'react';
import { Container, Menu, Button } from 'semantic-ui-react';

const NavBar = () => {
    return (
        <Menu fixed="top" inverted>
            <Container>
                <Menu.Item header>
                    <img src="assets/logo.png" alt="logo" />
                    Social Network
                </Menu.Item>

                <Menu.Item name='Activities' />
                <Menu.Item>
                    <Button positive content="Create Activity" />
                </Menu.Item>
            </Container>
        </Menu>
    )
};

export default NavBar;
