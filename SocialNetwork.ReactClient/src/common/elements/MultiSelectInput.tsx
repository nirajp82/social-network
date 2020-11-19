import React from 'react';
import { FieldRenderProps } from 'react-final-form';
import { FormFieldProps, Form, Select, Label, DropdownProps } from 'semantic-ui-react';

interface IProps extends FieldRenderProps<string, HTMLElement>, FormFieldProps {
    id: string,
    label?: string,
    onChange?: (data: any, event: React.SyntheticEvent<HTMLElement, Event>) => void
}

const SelectInput: React.FC<IProps> = ({
    id,
    input,
    label,
    options,
    width,
    placeholder,
    meta: { touched, error, },
    onChange
}) => {
    const getValue = function (data: any) {
        let value: any;

        if (input.multiple) {
            if (data)
                value = data.toString().split(',');
            else
                value = [];
            return value;
        }
        else {
            value = data ? data.toString() : undefined;
            return value;
        }
    };

    const onChangeHandler = (event: React.SyntheticEvent<HTMLElement, Event>, data: DropdownProps) => {
        const value = getValue(data.value);
        input.onChange(value);
        onChange?.(data.value, event);
    };

    return (
        <Form.Field error={touched && !!error} width={width}>
            {label && (<label>{label}</label>)}
            {options &&
                (<Select key={id}
                    placeholder={placeholder}
                    value={getValue(input.value)}
                    options={options}
                    clearable={true}
                    search={true}
                    multiple={input.multiple}

                    onChange={(event, data) => onChangeHandler(event, data)}
                >
                </Select>)
            }
            {touched && error && (
                <Label basic color='red'>
                    {error}
                </Label>
            )}
        </Form.Field>
    )
};

export default SelectInput;
