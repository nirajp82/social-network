import React from 'react';
import { FieldRenderProps } from 'react-final-form';
import { FormFieldProps, Form, Select, Label } from 'semantic-ui-react';

interface IProps extends FieldRenderProps<string, HTMLElement>, FormFieldProps { }

const SelectInput: React.FC<IProps> = ({
    input,
    options,
    width,
    placeholder,
    meta: { touched, error } }) => {
    return (
        <Form.Field error={touched && !!error} width={width}>
            <Select placeholder={placeholder}
                value={input.value}
                options={options}
                onChange={(_event, data) => input.onChange(data.value)}>
            </Select>
            {touched && error && (
                <Label basic color='red'>
                    {error}
                </Label>
            )}
        </Form.Field>
    )
};

export default SelectInput;