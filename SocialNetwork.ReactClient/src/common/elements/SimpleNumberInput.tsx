import React, { useState } from 'react';
import { Form, Label } from 'semantic-ui-react';

interface IProps {
    name: string,
    placeholder?: string,
    value?: number,
    required?: boolean,
    disabled?: boolean,
    hasError?: boolean,
    errorMessage?: string,
    onChange?: (data: any, event: React.ChangeEvent<HTMLInputElement>) => void,
};

const SimpleNumberInput: React.FC<IProps> = ({
    name,
    placeholder,
    value,
    required,
    disabled,
    hasError,
    errorMessage,
    onChange
}) => {
    const [val, setVal] = useState(value?.toString() ?? "");
    const [isInvalid, setIsInvalid] = useState(hasError);

    const onChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        let data: string | undefined = undefined;
        if (e.target.value)
            data = e.target.value;
        else
            data = "";

        setVal(data);

        if (required && (!!!data || data === ""))
            setIsInvalid(true);
        else
            setIsInvalid(false);

        onChange?.(data, e);
    };

    return (
        <Form.Field>
            <div className="ui input">
                <input name={name}
                    placeholder={placeholder}
                    required={required}
                    disabled={disabled}
                    className={hasError ? "Red" : ""}
                    style={{ width: "99%" }}
                    value={val}
                    type="number"
                    title={hasError ? "Has Error" : "No Error"}
                    onChange={(e) => onChangeHandler(e)}>
                </input>

            </div>
            {isInvalid && (
                <div>
                    <Label basic color='red'>
                        {errorMessage}
                    </Label>
                </div>
            )}
        </Form.Field>
    );
};

export default SimpleNumberInput;
