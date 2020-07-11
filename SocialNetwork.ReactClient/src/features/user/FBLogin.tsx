import React from 'react';
import { Button, Icon } from 'semantic-ui-react';
import FacebookLogin from 'react-facebook-login/dist/facebook-login-render-props';
import * as constants from '../../utils/constants';

interface IProps {
    fbResponseCallback: (response: any) => void
}

const FBLogin: React.FC<IProps> = ({ fbResponseCallback}) => {
    return (
        <div>
            <FacebookLogin
                appId={constants.FACEBOOK_APP_ID}
                fields="name, email, picture"
                callback={fbResponseCallback}
                render={(renderProps: any) => (
                    <Button onClick={renderProps.onClick} type='button' fluid color='facebook'>
                        <Icon name='facebook' />
                        Login with Facebook
                    </Button>
                )}
            />
        </div>
    );
};

export default FBLogin;