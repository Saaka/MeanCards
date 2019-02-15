import React, { Component } from 'react';
import { AuthService } from 'Services';
import { LoaderButton } from 'CommonComponents';
import queryString from 'query-string';

export class Login extends Component {
    authService = new AuthService();
    state = {
        email: '',
        password: '',
        isLoading: false
    };

    handleLogin = (event) => {
        event.preventDefault();
        this.displayLoader(true);

        this.authService
            .loginWithCredentials(this.state.email, this.state.password)
            .then(userData => {
                this.props.onLogin(userData);
                var searchValue = queryString.parse(this.props.location.search);
                if (searchValue && searchValue.redirect)
                    this.redirectToPath(searchValue.redirect);
                else
                    this.redirectToMainPage();
            })
            .catch(err => {
                this.displayLoader(false);
                alert(err);
            });
    };
    handleChange = (e) => {
        this.setState(
            {
                [e.target.name]: e.target.value
            }
        )
    };
    redirectToMainPage = () => {
        this.props.history.replace('/');
    };
    redirectToPath = (path) => {
        this.props.history.push(path);
    };
    displayLoader = (show) => {
        this.setState({
            isLoading: show
        });
    };

    componentDidMount = () => {
        if (this.props.user.isLoggedIn)
            this.redirectToMainPage();
    };

    render() {
        return (
            <div className="container">
                <div className="row justify-content-md-center">
                    <div className="col-md-9 col-lg-6">
                        <h1>Login</h1>
                        <br />
                        <form onSubmit={this.handleLogin}>
                            <div className="form-group">
                                <label htmlFor="email">Email</label>
                                <input type="login-email"
                                    className="form-control"
                                    id="login-email"
                                    placeholder="Email"
                                    name="email"
                                    onChange={this.handleChange} />
                            </div>
                            <div className="form-group">
                                <label htmlFor="login-password">Password</label>
                                <input type="password"
                                    className="form-control"
                                    id="login-password"
                                    placeholder="Password"
                                    name="password"
                                    onChange={this.handleChange} />
                            </div>
                            <LoaderButton type="submit" className="btn btn-primary" text="Login" isLoading={this.state.isLoading} />
                        </form>
                    </div>
                </div>
            </div>
        );
    };
};