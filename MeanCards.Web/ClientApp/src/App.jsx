import './App.scss';
import React, { Component } from 'react';
import { Route } from 'react-router';
import {withAuth} from 'AuthComponents';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Countdown } from './components/Countdown';
import { Counter } from './components/Counter';
import { Login, Logout } from 'AuthComponents';
import { MainMenu } from './components/MainMenu';

export default class App extends Component {
    static displayName = App.name;

    CounterWithAuth = withAuth(Counter);

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/menu' component={MainMenu} />
                <Route path='/counter' component={this.CounterWithAuth} />
                <Route path='/countdown' component={Countdown} />
                <Route path='/login' component={Login} />
                <Route path='/logout' component={Logout} />
            </Layout>
        );
    }
}
