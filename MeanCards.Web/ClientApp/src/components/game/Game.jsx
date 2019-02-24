import React, { useState, useEffect } from 'react';
import { GameRepository } from './GameRepository';
import { PlayersList } from './gameComponents/GameComponentsExports';
import { Loader } from 'CommonComponents';
import { Constants } from 'Services';

const Game = (props) => {
    const repository = new GameRepository();
    const [game, setGame] = useState({});
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
        setGame(gameData);
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
                    <div className="col-xs-12 col-md-8 col-lg-9">
                        <h1>{game.game.name}</h1>
                    </div>
                    <div className="col-xs-12 col-md-4 col-lg-3">
                        <PlayersList players={game.players}></PlayersList>
                    </div>
                </div>
            </div>
        );
    }

    return loading === true ? renderLoader() : renderGame();
}

export { Game };