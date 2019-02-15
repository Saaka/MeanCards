import './App.scss';
import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Login, Logout, withAuth } from './components/auth/AuthExports';
import { CreateGame, Game } from './components/game/GameExports';
import { MainMenu, Home, Countdown, Counter } from './components/tempComponents/TempExports';

export default class App extends Component {

    state = {
        user: {
            isLoggedIn: false
        }
    };

    componentWillMount = () => {

    };

    onLogin = (user) => {
        this.setState({
            user: {
                ...user,
                isLoggedIn: true
            }
        }, () => console.log(user));
    };

    onLogout = () => {
        this.setState({
            user: {
                isLoggedIn: false
            }
        });
    };

    CounterWithAuth = withAuth(Counter);
    CreateGameWithAuth = withAuth(CreateGame);
    GameWithAuth = withAuth(Game);

    render() {
        return (
            <Layout user={this.state.user}>
                <Route exact path='/' component={Home} />
                <Route path='/menu' component={MainMenu} />
                <Route path='/counter' component={this.CounterWithAuth} />
                <Route path='/countdown' component={Countdown} />
                <Route path='/login' render={(props) => <Login {...props} onLogin={this.onLogin} />} />
                <Route path='/logout' render={(props) => <Logout {...props} onLogout={this.onLogout} />} />
                <Route path='/createGame' component={this.CreateGameWithAuth} />
                <Route path='/game/:code' component={this.GameWithAuth} />
            </Layout>
        );
    }
}
