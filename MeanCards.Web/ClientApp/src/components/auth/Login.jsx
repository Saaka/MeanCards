import React, { Component } from 'react';
import { AuthService } from 'Services';
import { LoaderButton } from '../common/CommonComponents';

export class Login extends Component {
    authService = new AuthService();
    state = {
        email: '',
        password: '',
        isLoading: false
    };

    handleLogin = (event) => {
        event.preventDefault();
        console.log(this.state);
        this.enableLoader(true);

        this.authService
            .login(this.state.email, this.state.password)
            .then(userData => {
                console.log('logged in:', userData);
                this.goToMainPage();
            })
            .catch(err => {
                this.enableLoader(false);
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
    goToMainPage() {
        this.props.history.replace('/');
    };
    enableLoader(isLoading) {
        this.setState({
            isLoading: isLoading
        });
    };
    
    componentWillMount() {
        if (this.authService.isLoggedIn())
            this.goToMainPage();
    };

    render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-6 offset-md-3">
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