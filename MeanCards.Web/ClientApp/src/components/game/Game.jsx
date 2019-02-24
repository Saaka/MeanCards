import React, { useState, useEffect } from 'react';
import { GameRepository } from './GameRepository';
import { PlayersList, GameDetails } from './gameComponents/GameComponentsExports';
import { Loader } from 'CommonComponents';
import { Constants } from 'Services';
import './Game.scss';

const Game = (props) => {
    const repository = new GameRepository();
    const [gameInfo, setGameInfo] = useState({});
    const [loading, setLoading] = useState(true);

    useEffect(() => loadGame(), []);

    function loadGame() {
        repository
            .getGame(props.match.params.code)
            .then(resp => updateGame(resp.data))
            .catch(err => {
                props.history.push(Constants.Routes.GAMELIST, { error: err });
            });
    }

    function updateGame(gameData) {
        setGameInfo(gameData);
        setLoading(false);
    }

    function renderLoader() {
        return (
            <Loader isLoading={true} />
        );
    }

    function renderGame() {
        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="col-xs-12 col-sm-7 col-md-8 col-lg-8">
                        <h1>{gameInfo.game.name}</h1>
                    </div>
                    <div className="col-xs-12 col-sm-5 col-md-4 col-lg-4 game-extra-components">
                        <div><GameDetails details={gameInfo.game} /></div>
                        <div><PlayersList players={gameInfo.players} /> </div>
                    </div>
                </div>
            </div>
        );
    }

    return loading === true ? renderLoader() : renderGame();
}

export { Game };