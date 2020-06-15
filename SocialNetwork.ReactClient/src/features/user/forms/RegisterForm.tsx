import React, { useContext } from 'react';
import { Form, Button, Header } from 'semantic-ui-react';
import { Form as FinalForm, Field } from 'react-final-form';
import { combineValidators, isRequired, composeValidators, hasLengthGreaterThan, hasLengthLessThan } from 'revalidate';
import { FORM_ERROR } from 'final-form';

import { IRegister } from '../../../models/IUser';
import TextInput from '../../../common/elements/TextInput';
import { rootStoreContext } from '../../../stores/rootStore';
import createBrowserHistory from '../../../utils/createBrowserHistory';
import { isValidEmail, isValidPassword } from '../../../utils/customValidators';
import * as constants from '../../../utils/constants';
import ModelContainer, { modalSize } from '../../../common/modals/modalContainer';
import ErrorMessage from '../../../common/elements/ErrorMessage';

const RegisterForm = () => {
    const rootStoreObj = useContext(rootStoreContext);
    const userStoreObj = rootStoreObj.userStore;
    const onClose = () => {
        createBrowserHistory.push(constants.NAV_HOME);
    };

    const validationRules = combineValidators({
        firstName: composeValidators(
            isRequired('First name'),
            hasLengthGreaterThan(1)({ message: 'First name must contain atleast 2 characters!' }),
            hasLengthLessThan(25)({ message: 'First name must be 24 characters or less!' })
        )(),
        lastName: composeValidators(
            isRequired('Last name'),
            hasLengthGreaterThan(1)({ message: 'Last name must contain atleast 2 characters!' }),
            hasLengthLessThan(25)({ message: 'Last name must be 24 characters or less!' })
        )(),
        email: composeValidators(
            isRequired('Email'),
            isValidEmail
        )(),
        userName: composeValidators(
            isRequired('User name'),
            hasLengthGreaterThan(5)({ message: 'User name must contain atleast 6 characters!' }),
            hasLengthLessThan(25)({ message: 'User name must be 24 characters or less!' })
        )(),
        password: composeValidators(
            isRequired('Password'),
            hasLengthGreaterThan(5)({ message: 'Password must contain atleast 6 characters!' }),
            hasLengthLessThan(11)({ message: 'Password must be 10 characters or less!' }),
            isValidPassword
        )()
    });

    const onRegisterHandler = async (values: IRegister) => {
        try {
            await userStoreObj.register(values);
            createBrowserHistory.push(constants.NAV_HOME);
        } catch (err) {
            return { [FORM_ERROR]: err };
        }
    };

    const getModalContent = () => {
        return (
            <FinalForm
                onSubmit={onRegisterHandler}
                validate={validationRules}
                render={(props) => (
                    <Form>
                        <Header
                            as="h2"
                            content="Register"
                            color="teal"
                            textAlign="center" />

                        <Field name="firstName"
                            component={TextInput}
                            placeholder='First Name' />

                        <Field name="lastName"
                            component={TextInput}
                            placeholder='Last Name' />

                        <Field name="email"
                            component={TextInput}
                            placeholder='Email' />

                        <Field name="userName"
                            component={TextInput}
                            placeholder='User Name' />

                        <Field name="password"
                            type="Password"
                            component={TextInput}
                            placeholder='Password' />

                        {props.submitError && !props.dirtySinceLastSubmit &&
                            (<ErrorMessage error={props.submitError} />)
                        }

                        <Button
                            loading={props.submitting}
                            disabled={(props.invalid && !props.dirtySinceLastSubmit) || props.pristine}
                            onClick={props.handleSubmit}
                            content="Register"
                            color="teal"
                            fluid
                        />
                        {/*{<pre>{JSON.stringify(props.form.getState(), null, 2)}</pre>} */}
                    </Form>
                )}
            />
        );
    };

    return (
        <ModelContainer
            defaultOpen={true}
            content={getModalContent()}
            onClose={onClose}
            size={modalSize.Tiny}
        />
    );
};

export default RegisterForm;