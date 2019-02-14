import React, { Component } from 'react';
import { Loader } from 'CommonComponents';

export class Game extends Component {
    state = {
        isLoading: true,
        gameCode: "",
        game:{
            name: "Game name"
        }
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
                <h2>{this.state.game.name}</h2>
                <button className="btn btn-primary"
                        onClick={this.updateGame}>Update</button>
                <Loader isLoading={this.state.isLoading} />
            </div>
        );
    }
}