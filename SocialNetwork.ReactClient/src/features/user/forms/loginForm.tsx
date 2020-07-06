import React, { Fragment, useContext, useEffect } from 'react';
import { Form, Button, Header } from 'semantic-ui-react';
import { Form as FinalForm, Field } from 'react-final-form';
import { combineValidators, isRequired } from 'revalidate';
import { FORM_ERROR } from 'final-form';

import { ILogin } from '../../../models/IUser';
import TextInput from '../../../common/elements/TextInput';
import { rootStoreContext } from '../../../stores/rootStore';
import createBrowserHistory from '../../../utils/createBrowserHistory';
import * as constants from '../../../utils/constants';
import ModelContainer, { modalSize } from '../../../common/modals/modalContainer';
import ErrorMessage from '../../../common/elements/ErrorMessage';

const LoginForm = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const userStoreObj = rootStoreObj.userStore;
    const commonStoreObj = rootStoreObj.commonStore;

    const validationRules = combineValidators({
        userName: isRequired('User name'),
        password: isRequired('Password')
    });

    const redirectToHomePage = () => {
        createBrowserHistory.push(constants.NAV_HOME);
    };

    useEffect(() => {
        // If user is already logged in, redirect user to home page.
        if (userStoreObj.canAccessSecureResource) {
            redirectToHomePage();
        }
    }, [userStoreObj.canAccessSecureResource]);

    const onLoginHandler = async (values: ILogin) => {
        try {
            await userStoreObj.login(values);
            createBrowserHistory.push(constants.NAV_ACTIVITIES);
        } catch (err) {
            //Return Form Error to React Final Form, It will populate the submitError prop
            return { [FORM_ERROR]: err.statusText }
        }
    };

    const getContent = () => {
        return (<FinalForm
            onSubmit={onLoginHandler}
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

                    {props.submitError && !props.dirtySinceLastSubmit &&
                        (<ErrorMessage error={props.submitError} text='Invalid user name or password' />)
                    }

                    <Button
                        loading={props.submitting}
                        disabled={(props.invalid && !props.dirtySinceLastSubmit) || props.pristine}
                        onClick={props.handleSubmit}
                        content="Login"
                        color="teal"
                        fluid
                    />
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
                onClose={redirectToHomePage}
                size={modalSize.Tiny} />
        </Fragment>
    );

};

export default LoginForm;