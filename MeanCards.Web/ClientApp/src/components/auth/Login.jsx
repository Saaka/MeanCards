import React, { Component } from 'react';
import { AuthService } from 'Services';
import queryString from 'query-string';
import { Alert } from 'reactstrap'
import { Trans } from 'react-i18next';
import { LoginWithCredentials } from './LoginWithCredentials';
import { LoginWithGoogle } from './LoginWithGoogle';
import { Loader } from 'CommonComponents';
import './Login.scss';

export class Login extends Component {
    authService = new AuthService();
    state = {
        isLoading: false,
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
        this.toggleLoader(false);
    };

    toggleError = (show, error) => {
        this.setState({
            showError: show,
            error: error
        });
    };

    toggleLoader = (showLoader) => {
        this.setState({
            isLoading: showLoader
        });
    };

    setLoginWithCredentials = (show) => {
        this.setState({
            loginWithCredentials: show
        });
    };

    renderLoginOptions = () => {
        if (this.state.loginWithCredentials)
            return (
                <LoginWithCredentials onLoggedIn={this.onLoggedIn} onError={this.onError} onGoBack={() => this.setLoginWithCredentials(false)}></LoginWithCredentials>
            );
        else
            return this.renderMainLoginPanel();
    }

    renderMainLoginPanel = () => {
        return (
            <div>
                <div className="row justify-content-center">
                    <LoginWithGoogle onLoggedIn={this.onLoggedIn} onError={this.onError} showLoader={() => this.toggleLoader(true)}></LoginWithGoogle>
                </div>
                <br />
                <div className="row justify-content-center invisible">
                    <button className="btn btn-primary login-button" onClick={() => this.setLoginWithCredentials(true)}>Login</button>
                </div>
            </div>
        );
    };

    render() {
        return (
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-md-9 col-lg-6">
                        <h1 className="text-center">Login</h1>
                        <br />
                        {this.renderLoginOptions()}
                        <br />
                        <Alert color="danger" isOpen={this.state.showError} toggle={() => this.toggleError(false)}><Trans>{this.state.error}</Trans></Alert>
                    </div>
                </div>
                <Loader isLoading={this.state.isLoading}></Loader>
            </div>
        );
    };
};