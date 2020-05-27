import React from 'react';
import { Dimmer, Loader } from 'semantic-ui-react';

const ProgressBar: React.FC<{
    message: string,
    inverted?: boolean
}> = ({ message = "Loading", inverted = true }) => {
        return (
            <Dimmer active inverted={inverted}>
                <Loader  content={message}/>
            </Dimmer>
        )    
};

export default ProgressBar;