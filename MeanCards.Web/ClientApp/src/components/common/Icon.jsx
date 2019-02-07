import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

const Icon = (props) => {

    return (
        <FontAwesomeIcon icon={props.icon} spin={props.spin} />
    );
};

export { Icon };