import React, { Component } from 'react';
import {AuthService} from '../services/Services';

export class Login extends Component {
    authService = new AuthService();
    state = {
        email: '',
        password: ''
    };

    handleLogin = (event) => {
        event.preventDefault();
        console.log(this.state);
        
        this.authService
            .login(this.state.email, this.state.password)
            .then(userData => {
                console.log(userData);
                alert(`Logged in as ${userData.name}`);
            })
            .catch(err => {
                alert(err);
            })
    };
    handleChange = (e) => {
        this.setState(
            {
                [e.target.name]: e.target.value
            }
        )
    };

    render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-6 offset-md-3">
                        <h1>Login</h1>
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
                            <button type="submit" className="btn btn-primary">Login</button>
                        </form>
                    </div>
                </div>
            </div>
        );
    }
}