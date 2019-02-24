import React from 'react';
import './Avatar.scss';

const Avatar = (props) => {

    return (
        <img src={props.imageUrl} alt="Avatar" className="img-thumbnail img-avatar" />
    );
};

export { Avatar };