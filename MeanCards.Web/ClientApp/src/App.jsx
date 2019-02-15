import './App.scss';
import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Login, Logout, withAuth } from './components/auth/AuthExports';
import { CreateGame, Game } from './components/game/GameExports';
import { MainMenu, Home, Countdown, Counter } from './components/tempComponents/TempExports';
import { Loader } from 'CommonComponents';
import { AuthService } from 'Services';

export default class App extends Component {
    authService = new AuthService();

    state = {
        isLoading: true,
        user: {
            isLoggedIn: false
        }
    };

    componentDidMount = () => {
        if (this.authService.isLoggedIn())
            this.loadUserData();
        else
            this.hideLoader();
    };

    loadUserData = () => {
        this.authService
            .getUser()
            .then(this.setUser)
            .finally(this.hideLoader);
    }

    onLogin = (user) => this.setUser(user);

    setUser = (user) => {
        this.setState({
            user: {
                ...user,
                isLoggedIn: true
            }
        });
    };

    onLogout = () => {
        this.setState({
            user: {
                isLoggedIn: false
            }
        });
    };

    hideLoader = () => this.setState({ isLoading: false });

    renderAuthComponent = (props, comp) => {
        var AuthComponent = withAuth(comp); 
        return (
            <AuthComponent {...props} user={this.state.user} />
        );
    }

    render() {
        return (
            <div>
                <Layout user={this.state.user}>
                    <Route exact path='/' component={Home} />
                    <Route path='/menu' component={MainMenu} />
                    <Route path='/counter' render={(props) => this.renderAuthComponent(props, Counter)} />
                    <Route path='/countdown' component={Countdown} />
                    <Route path='/login' render={(props) => <Login {...props} onLogin={this.onLogin} />} />
                    <Route path='/logout' render={(props) => <Logout {...props} onLogout={this.onLogout} />} />
                    <Route path='/createGame' render={(props) => this.renderAuthComponent(props, CreateGame)} />
                    <Route path='/game/:code' render={(props) => this.renderAuthComponent(props, Game)} />
                </Layout>
                <Loader isLoading={this.state.isLoading} />
            </div>
        );
    }
}
