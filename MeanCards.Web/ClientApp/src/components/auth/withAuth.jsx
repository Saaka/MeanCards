import React, { Component } from 'react';

const withAuth = (AuthComponent) => {

    return class AuthWrapper extends Component {

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
            if (!this.props.user.isLoggedIn) {
                this.goToLogin(true);
            }
        };

        render = () => {
            if (this.props.user.isLoggedIn) {
                return (
                    <AuthComponent history={this.props.history} user={this.props.user} {...this.props} />
                );
            }
            else {
                return null;
            }
        }
    };
}

export { withAuth };