import React from 'react';
import { Icon, Avatar } from 'CommonComponents';

const UserAvatar = (props) => {

    const renderDefault = () => (<Icon icon="user"></Icon>);
    const renderAvatar = () => (<Avatar imageUrl={props.user.imageUrl} />);
    return props.user.isLoggedIn ?
        renderAvatar() :
        renderDefault();
};

export { UserAvatar };