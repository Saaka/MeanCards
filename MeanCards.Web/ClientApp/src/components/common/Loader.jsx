import React from 'react';
import './Loader.scss';

const Loader = (props) => {

    return (
        <span>
            {props.isLoading ? <div className="loading">Loading&#8230;</div> : <div></div>}
        </span>
    );
};

export { Loader };