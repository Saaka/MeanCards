import React, { Component } from 'react';
import { AuthService } from 'Services';
import queryString from 'query-string';
import { Alert } from 'reactstrap'
import { Trans } from 'react-i18next';
import { LoginWithCredentials } from './LoginWithCredentials';
import { LoginWithGoogle } from './LoginWithGoogle';

export class Login extends Component {
    authService = new AuthService();
    state = {
        showError: false,
        error: "",
        loginWithCredentials: false
    };

    onLoggedIn = (userData) => {
        this.props.onLogin(userData);
        var searchValue = queryString.parse(this.props.location.search);
        if (searchValue && searchValue.redirect)
            this.redirectToPath(searchValue.redirect);
        else
            this.redirectToMainPage();
    };

    redirectToMainPage = () => {
        this.props.history.replace('/');
    };
    redirectToPath = (path) => {
        this.props.history.push(path);
    };

    componentDidMount = () => {
        if (this.props.user.isLoggedIn)
            this.redirectToMainPage();
    };

    onError = (error) => {
        this.toggleError(true, error);
    };

    toggleError = (show, error) => {
        this.setState({
            showError: show,
            error: error
        });
    };

    renderLoginOptions = () => {
        if (this.state.loginWithCredentials)
            return (
                <LoginWithCredentials onLoggedIn={this.onLoggedIn} onError={this.onError}></LoginWithCredentials>
            );
        else
            return this.renderMainLoginPanel();
    }

    renderMainLoginPanel = () => {
        return (
            <div>
                <div className="row justify-content-center">
                    <LoginWithGoogle onLoggedIn={this.onLoggedIn} onError={this.onError}></LoginWithGoogle>
                </div>
                <br />
                <div className="row justify-content-center">
                    <button className="btn btn-secondary" onClick={() => this.setState({ loginWithCredentials: true })}>Login with credentials</button>
                </div>
            </div>
        );
    };

    render() {
        return (
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-md-9 col-lg-6">
                        <h1>Login</h1>
                        <br />
                        {this.renderLoginOptions()}
                        <br />
                        <Alert color="danger" isOpen={this.state.showError} toggle={() => this.toggleError(false)}><Trans>{this.state.error}</Trans></Alert>
                    </div>
                </div>
            </div>
        );
    };
};