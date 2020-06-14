import React, { useContext } from 'react';
import { Form, Button, Label } from 'semantic-ui-react';
import { Form as FinalForm, Field } from 'react-final-form';
import { observer } from 'mobx-react-lite';
import { combineValidators, isRequired } from 'revalidate';
import { FORM_ERROR } from 'final-form';

import { ILogin } from '../../../models/IUser';
import TextInput from '../../../common/form/TextInput';
import { rootStoreContext } from '../../../stores/rootStore';
import createBrowserHistory from '../../../utils/createBrowserHistory';
import * as constants from '../../../utils/constants';


const validationRules = combineValidators({
    userName: isRequired('User name'),
    password: isRequired('Password')
});

const LoginForm = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const userStoreObj = rootStoreObj.userStore;
    const onFormSubmit = async (command: ILogin) => {
        try {
            await userStoreObj.login(command);
            createBrowserHistory.push(constants.NAV_ACTIVITIES);
        } catch (err) {
            //Return Form Error to React Final Form, It will populate the submitError prop
            return { [FORM_ERROR]: err }
        }
    };

    return (
        <FinalForm
            onSubmit={onFormSubmit}
            validate={validationRules}
            render={(props) => (
                <Form onSubmit={props.handleSubmit}>
                    <Field name="userName"
                        component={TextInput}
                        placeholder='User Name' />

                    <Field name="password"
                        type="Password"
                        component={TextInput}
                        placeholder='Password' />

                    {props.submitError && !props.dirtySinceLastSubmit &&
                        <Label color='red' basic content={props.submitError} />}
                    <br />
                    <Button
                        loading={props.submitting}
                        disabled={(props.invalid && !props.dirtySinceLastSubmit) || props.pristine}
                        type="Submit"
                        content="Login"
                        positive />
                        {/*
                            *<pre>{JSON.stringify(props.form.getState(), null, 2)} </pre> 
                        */}
                </Form>
            )}
        />
    );
};

export default observer(LoginForm);