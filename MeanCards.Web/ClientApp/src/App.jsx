import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Countdown } from './components/Countdown';
import { Counter } from './components/Counter';
import { Login, Logout } from 'AuthComponents';
import './App.scss';

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
