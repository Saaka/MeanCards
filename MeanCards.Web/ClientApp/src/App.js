import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Countdown } from './components/Countdown';
import { Counter } from './components/Counter';
import { Login } from './components/Components';
import { Logout } from './components/auth/Logout';


export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/countdown' component={Countdown} />
                <Route path='/login' component={Login} />
                <Route path='/logout' component={Logout} />
            </Layout>
        );
    }
}
