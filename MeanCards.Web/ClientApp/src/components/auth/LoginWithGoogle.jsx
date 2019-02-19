import React from 'react';
import { AuthService, ConfigService } from 'Services';
import { GoogleLogin } from 'react-google-login';

const LoginWithGoogle = (props) => {
    const configService = new ConfigService();
    const authService = new AuthService();

    function onLogin(response) {
        authService
            .loginWithGoogle(response.tokenId)
            .then(props.onLoggedIn)
            .catch(props.onError);
    }

    function onLoginFail(response) {
        props.onError(response.error);
    }

    return (
        <GoogleLogin
            clientId={configService.GoogleClientId}
            onSuccess={onLogin}
            onFailure={onLoginFail}></GoogleLogin>
    );
};

export { LoginWithGoogle };