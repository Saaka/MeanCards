import React from 'react';
import { Icon } from 'CommonComponents';

const LoaderButton = (props) => {

    const isLoading = () => {
        return props.isLoading;
    };

    return (
        <button type={props.type}
            className={props.className}
            disabled={isLoading() || props.disabled}>
            {props.text} {isLoading() ? <Icon icon="spinner" spin /> : ""}
        </button>
    );
};

export { LoaderButton };