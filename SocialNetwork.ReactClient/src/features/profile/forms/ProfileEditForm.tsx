﻿import React, { useState } from 'react';
import { Form, Tab, Button } from 'semantic-ui-react';
import { Form as FinalForm, Field } from 'react-final-form';
import { FORM_ERROR } from 'final-form';
import { combineValidators, isRequired, composeValidators, hasLengthGreaterThan, hasLengthLessThan } from 'revalidate';
import { observer } from 'mobx-react-lite';

import { isValidEmail } from '../../../utils/customValidators';
import TextInput from '../../../common/elements/TextInput';
import TextAreaInput from '../../../common/elements/TextAreaInput';
import { IProfile } from '../../../models/IProfile';

interface IProps {
    updateProfile: (profile: IProfile) => Promise<void>,
    userProfile: IProfile,
    setEditMode: (value:boolean) => void
};

const ProfileEditForm: React.FC<IProps> = ({ updateProfile, userProfile, setEditMode }) => {
    const [isSaving, setIsSaving] = useState(false);

    const onUpdateProfileHandler = async (profile: IProfile) => {
        try {
            setIsSaving(true);
            await updateProfile(profile);
            setEditMode(false);
            setIsSaving(false);
        } catch (err) {
            return { [FORM_ERROR]: err };
        }
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
        bio: hasLengthLessThan(241)({ message: 'Bio must be 240 characters or less!' })
    });

    if (!userProfile)
        return <Tab.Pane>Loading user profile data</Tab.Pane>

    //console.log(toJS(userProfile));
    return (
        <FinalForm
            onSubmit={onUpdateProfileHandler}
            validate={validationRules}
            initialValues={userProfile}
            render={(props) => (
                <Form onSubmit={props.handleSubmit}>
                    <Field name="firstName"
                        component={TextInput}
                        value={userProfile?.firstName}
                        placeholder='First Name' />

                    <Field name="lastName"
                        component={TextInput}
                        value={userProfile?.lastName}
                        placeholder='Last Name' />

                    <Field name="email"
                        component={TextInput}
                        value={userProfile?.email}
                        placeholder='Email' />

                    <Field name="bio"
                        component={TextAreaInput}
                        value={userProfile?.bio}
                        placeholder='bio' />

                    <Button
                        floated="right"
                        type="Submit"
                        loading={isSaving}
                        disabled={props.invalid || props.pristine}
                        positive content="Submit" />
                </Form>
            )}
        />
    )
};

export default observer(ProfileEditForm);