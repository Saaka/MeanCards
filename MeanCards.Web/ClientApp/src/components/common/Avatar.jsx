import React from 'react';
import { Icon } from 'CommonComponents';
import './Avatar.scss';

const Avatar = (props) => {

    const renderDefault = () => (<Icon icon="user"></Icon>);
    const renderAvatar = () => (<img src={props.user.imageUrl} alt="Avatar" className="img-thumbnail img-avatar" />);
    return props.user.isLoggedIn ?
        renderAvatar() :
        renderDefault();
};

export { Avatar };