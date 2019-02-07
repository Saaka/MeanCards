import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

const LoaderButton = (props) => {

    const isLoading = () => {
        return props.isLoading;
    };

    return (
        <button type={props.type} 
            className={props.className} 
            disabled={isLoading() || props.disabled}>
                {props.text} {isLoading() ? <FontAwesomeIcon icon="spinner" spin /> : ""}
        </button>
    );
};

export { LoaderButton };