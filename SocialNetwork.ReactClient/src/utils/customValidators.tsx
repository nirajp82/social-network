import { createValidator } from 'revalidate';

export const isValidEmail = createValidator(
    message => value => {
        if (value && !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(value)) {
            return message
        }
    },
    'Invalid email address'
)

export const isValidPassword = createValidator(
    message => value => {
        if (value && !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,10}$$/g.test(value)) {
            return message
        }
    },
    'Password requires atleast 1 lower case letter, 1 capital letter, 1 digit, 1 special character' +
        ' and the length should be between 6-10 characters.'
)

export const isGreaterThan = (n: number) => createValidator(
    message => value => {
        if (value && Number(value) <= n) {
            return message
        }
    },
    field => `${field} must be greater than ${n}`
)