import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

const Icon = (props) => {

    const brands = ["google"];
    function getIcon() {
        if (brands.indexOf(props.icon) === 0)
            return ["fab", props.icon];

        return props.icon;
    }

    return (
        <FontAwesomeIcon icon={getIcon()} spin={props.spin} />
    );
};

export { Icon };