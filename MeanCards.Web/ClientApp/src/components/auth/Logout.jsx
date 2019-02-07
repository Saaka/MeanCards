import React, { Component } from 'react';
import { AuthService } from '../services/Services';
import { Icon } from '../common/CommonComponents';

export class Logout extends Component {
    authService = new AuthService();

    componentWillMount() {
        this.authService.logout();
        this.props.history.replace('/login');
    };

    render() {
        return (<div className="container">
            <div className="row">
                <div className="col-md-6 offset-md-3">
                    <h1><Icon icon="spinner" spin/></h1>
                </div>
            </div>
        </div>
        );
    };
};