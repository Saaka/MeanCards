import React from 'react';
import { Icon } from 'CommonComponents';

const Avatar = (props) => {

    const renderDefault = () => (<Icon icon="user"></Icon>);
    const renderAvatar = () => (<img src={props.user.imageUrl} alt="Avatar" style={{"border-radius": "50%", "width" : "35px", "border": "3px solid grey"}} />);
    return props.user.isLoggedIn ?
        renderAvatar() :
        renderDefault();
};

export { Avatar };