import React, { useContext, Fragment, useState } from 'react';
import { Form, Button, Label, Header } from 'semantic-ui-react';
import { Form as FinalForm, Field } from 'react-final-form';
import { observer } from 'mobx-react-lite';
import { combineValidators, isRequired } from 'revalidate';
import { FORM_ERROR } from 'final-form';

import { ILogin } from '../../../models/IUser';
import TextInput from '../../../common/elements/TextInput';
import { rootStoreContext } from '../../../stores/rootStore';
import createBrowserHistory from '../../../utils/createBrowserHistory';
import * as constants from '../../../utils/constants';
import ModelContainer from '../../../common/modals/modalContainer';
import { modalSize } from '../../../common/modals/modalContainer';

const validationRules = combineValidators({
    userName: isRequired('User name'),
    password: isRequired('Password')
});

const LoginForm = () => {
    const [open, setOpen] = useState(false);
    const rootStoreObj = useContext(rootStoreContext);
    const userStoreObj = rootStoreObj.userStore;
    const onFormSubmit = async (command: ILogin) => {
        try {
            await userStoreObj.login(command);
            createBrowserHistory.push(constants.NAV_ACTIVITIES);
        } catch (err) {
            //Return Form Error to React Final Form, It will populate the submitError prop
            return { [FORM_ERROR]: err.statusText }
        }
    };

    const onClose = () => {
        createBrowserHistory.push(constants.NAV_HOME);
    };

    const getContent = () => {
        return (<FinalForm
            onSubmit={onFormSubmit}
            validate={validationRules}
            render={(props) => (
                <Form>
                    <Header
                        as="h2"
                        content="Login to Social Network"
                        color="teal"
                        textAlign="center" />

                    <Field name="userName"
                        component={TextInput}
                        placeholder='User Name' />

                    <Field name="password"
                        type="Password"
                        component={TextInput}
                        placeholder='Password' />

                    <Button
                        loading={props.submitting}
                        disabled={(props.invalid && !props.dirtySinceLastSubmit) || props.pristine}
                        onClick={props.handleSubmit}
                        content="Login"
                        color="teal"
                        fluid
                    />

                    {props.submitError && !props.dirtySinceLastSubmit &&
                        <Fragment>
                            <br />
                            <Label color='red' basic content={props.submitError} />
                        </Fragment>
                    }
                    {/*
                                *<pre>{JSON.stringify(props.form.getState(), null, 2)} </pre> 
                            */}
                </Form>
            )}
        />);
    };

    return (
        <Fragment>
            <ModelContainer
                defaultOpen={true}
                content={getContent()}
                onClose={onClose}
                size={modalSize.Tiny} />
        </Fragment>
    );

};

export default observer(LoginForm);