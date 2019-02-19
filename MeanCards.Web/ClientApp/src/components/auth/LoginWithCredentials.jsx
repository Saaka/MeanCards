import React, { useState } from 'react';
import { LoaderButton } from 'CommonComponents';
import { AuthService } from 'Services';

const LoginWithCredentials = (props) => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const authService = new AuthService();

    const handleLogin = (event) => {
        event.preventDefault();
        setIsLoading(true);

        authService
            .loginWithCredentials(email, password)
            .then(props.onLoggedIn)
            .catch(err => {
                setIsLoading(false);
                props.onError(err);
            });
    };
    return (
        <div>
            <form onSubmit={handleLogin}>
                <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <input type="login-email"
                        className="form-control"
                        id="login-email"
                        placeholder="Email"
                        name="email"
                        onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div className="form-group">
                    <label htmlFor="login-password">Password</label>
                    <input type="password"
                        className="form-control"
                        id="login-password"
                        placeholder="Password"
                        name="password"
                        onChange={(e) => setPassword(e.target.value)} />
                </div>
                <LoaderButton type="submit" className="btn btn-primary" text="Login" isLoading={isLoading} />
            </form>
        </div>
    );
};

export { LoginWithCredentials };

