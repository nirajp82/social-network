import React from 'react';
import { Container, Menu, Button } from 'semantic-ui-react';

interface IProps {
    onCreateActivity: () => void
};

const NavBar: React.FC<IProps> = ({ onCreateActivity }) => {
    return (
        <Menu fixed="top" inverted>
            <Container>
                <Menu.Item header>
                    <img src="assets/logo.png" alt="logo" />
                    Social Network
                </Menu.Item>

                <Menu.Item name='Activities' />
                <Menu.Item>
                    <Button onClick={() => onCreateActivity()} content="Create Activity" positive />
                </Menu.Item>
            </Container>
        </Menu>
    )
};

export default NavBar;
