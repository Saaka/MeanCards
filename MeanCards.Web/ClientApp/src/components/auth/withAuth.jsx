import React, { Component } from 'react';
import { AuthService } from '../services/Services';

const withAuth = (AuthComponent) =>  {
    const authService = new AuthService();

    return class AuthWrapper extends Component {
        state = {
            user: null
        };

        goToLogin() {
            this.props.history.replace('/login');
        };

        componentWillMount() {
            if (!authService.isLoggedIn()) {
                this.goToLogin();
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
            if(this.state.user) {
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