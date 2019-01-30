import React, { Component } from 'react';
import { AuthService } from '../services/Services';

export function Logout(props) {
    const authService = new AuthService();

    authService.logout();
    props.history.replace('/login');

    return (
        <div className="container">
            <div className="row">
                <div className="col-md-6 offset-md-3">
                    <h1>Logging out</h1>
                </div>
            </div>
        </div>
    );
};