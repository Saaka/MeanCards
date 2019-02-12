import React, { Component } from 'react';
import { AuthService } from 'Services';

const withAuth = (AuthComponent) => {
    const authService = new AuthService();

    return class AuthWrapper extends Component {
        state = {
            user: null
        };

        goToLogin = (useRedirect) => {
            if (useRedirect) {
                var redirect = this.props.location.pathname;
                this.props.history.replace(`/login?redirect=${redirect}`);
            }
            else {
                this.props.history.replace('/');
            }
        };

        componentWillMount = () => {
            if (!authService.isLoggedIn()) {
                this.goToLogin(true);
            }
            else {
                try {
                    const profile = authService.getTokenData();
                    this.setState({
                        user: profile
                    });
                }
                catch (err) {
                    authService.logout();
                    this.goToLogin();
                }
            }
        };

        render() {
            if (this.state.user) {
                return (
                    <AuthComponent history={this.props.history} user={this.state.user} {...this.props} />
                );
            }
            else {
                return null;
            }
        }
    };
}

export { withAuth };