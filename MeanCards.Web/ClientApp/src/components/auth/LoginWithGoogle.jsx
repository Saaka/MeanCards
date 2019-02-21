import React from 'react';
import { AuthService, ConfigService } from 'Services';
import { GoogleLogin } from 'react-google-login';
import { Icon } from 'CommonComponents';

const LoginWithGoogle = (props) => {
    const configService = new ConfigService();
    const authService = new AuthService();

    function onLogin(response) {
        props.showLoader();
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
            onFailure={onLoginFail}
            render={props => (
                <button className="btn btn-light login-button"
                    onClick={props.onClick}>
                    <Icon icon="google"></Icon> Sign in with Google
                </button>
            )}></GoogleLogin>
    );
};

export { LoginWithGoogle };