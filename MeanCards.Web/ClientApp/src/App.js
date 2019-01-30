import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Countdown } from './components/Countdown';
import { CounterWithAuth } from './components/Counter';
import { Login } from './components/auth/AuthComponents';


export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={CounterWithAuth} />
                <Route path='/countdown' component={Countdown} />
                <Route path='/login' component={Login} />
            </Layout>
        );
    }
}
