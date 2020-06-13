import React, { useContext } from 'react';
import { Form, Button } from 'semantic-ui-react';
import { Form as FinalForm, Field } from 'react-final-form';
import { observer } from 'mobx-react-lite';

import { ILogin } from '../../../models/IUser';
import TextInput from '../../../common/form/TextInput';
import { rootStoreContext } from '../../../stores/rootStore';

const LoginForm = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const userStoreObj = rootStoreObj.userStore;
    const onFormSubmit = (command: ILogin) => {
        userStoreObj.login(command);
    };

    return (
        <FinalForm
            onSubmit={onFormSubmit}
            render={(props) => (
                <Form onSubmit={props.handleSubmit}>
                    <Field name="UserName"
                        component={TextInput}
                        placeholder='User Name' />

                    <Field name="Password"
                        type="Password"
                        component={TextInput}
                        placeholder='Password' />

                    <Button type="Submit" content="Login" positive />
                </Form>
            )}
        />
    );
};

export default observer(LoginForm);
