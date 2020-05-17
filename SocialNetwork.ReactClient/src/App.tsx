import React, { useState, useEffect } from 'react';
import { Header, Icon, List } from 'semantic-ui-react';
import Axios from 'axios';

function App() {
    const [values, setValues] = useState([]);

    const fetchValues = async () => {
        const response: any = await Axios.get("http://localhost/socialnetwork/api/Values", { withCredentials: true });
        setValues(response.data);
    };

    useEffect(() => {
        fetchValues();
    }, []);

    return (
        <React.Fragment>
            <Header as="h2">
                <Icon name="users" />
                <Header.Content>Social Networking - Sample API Call</Header.Content>
            </Header>
            <List>
                {
                    values.map((item: any) => {
                        return (
                            <List.Item key={item.id}>
                                {item.name}
                            </List.Item>
                        );
                    })
                }
            </List>
        </React.Fragment>
    );
}

export default App;