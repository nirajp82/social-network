import React, { useState, useEffect } from 'react';
import { Form, Select, Label, DropdownItemProps, DropdownProps } from 'semantic-ui-react';

interface IProps {
    name: string,
    placeholder?: string,
    options: DropdownItemProps[],
    value?: any,
    onChange?: (data: any, event: React.SyntheticEvent<HTMLElement, Event>) => void,
    required?: boolean,
    disabled?: boolean,
    hasError?: boolean,
    errorMessage?: string,
}

const SimpleSelectInput: React.FC<IProps> = ({
    name,
    options,
    placeholder,
    value,
    required,
    disabled,
    hasError,
    errorMessage,
    onChange
}) => {
    const [selectedValue, setSelectedValue] = useState(value);
    const [isInvalid, setIsInvalid] = useState(hasError);

    const onChangeHandler = (event: React.SyntheticEvent<HTMLElement, Event>, data: DropdownProps) => {
        setSelectedValue(data.value);

        if (required && !!!data)
            setIsInvalid(true);
        else
            setIsInvalid(false);

        console.log(hasError);
        onChange?.(data.value, event);
    };

    useEffect(() => {
        setSelectedValue(value);
    }, [value]);

    return (
        <Form.Field>
            <Select name={name}
                placeholder={placeholder}
                value={selectedValue}
                options={options}
                required={required}
                disabled={disabled}
                className={hasError ? "Red" : ""}
                style={{ width: "99%" }}
                onChange={(event, data) => onChangeHandler(event, data)}>
            </Select>
            {isInvalid && (
                <Label basic color='red'>
                    {errorMessage}
                </Label>
            )}
        </Form.Field>
    )
};

export default SimpleSelectInput;
