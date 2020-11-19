import React from 'react';
import { FieldRenderProps } from 'react-final-form';
import { FormFieldProps, Form, Label } from 'semantic-ui-react';

interface IProps extends FieldRenderProps<number, HTMLElement>, FormFieldProps {}

const NumberInput: React.FC<IProps> = ({
    input,
    width,
    type,
    placeholder,
    required,
    meta: { touched, error }
}) => {
    return (
        <Form.Field error={touched && !!error} type={type} width={width}>
            <label>{placeholder}</label>
            <input {...input} placeholder={placeholder} type="number" />
            {touched && error && (
                <Label basic color='red'>
                    {error}
                </Label>
            )}
        </Form.Field>
    );
};

export default NumberInput;
