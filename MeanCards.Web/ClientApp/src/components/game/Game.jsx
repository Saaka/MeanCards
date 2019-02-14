import React, { Component } from 'react';
import { Loader } from 'CommonComponents';

export class Game extends Component {
    state = {
        isLoading: true,
        gameCode: ""
    };
    
    componentDidMount = () => {

        this.setState({
            gameCode: this.props.match.params.code,
            isLoading: false
        });
    };

    render() {
        return (
            <div>
                <h1>Game {this.state.gameCode}</h1>
                <Loader isLoading={this.state.isLoading} />
            </div>
        );
    }
}