import React, { useState, useEffect } from 'react';
import { GameRepository } from './GameRepository';
import { Loader } from 'CommonComponents';
import { Constants } from 'Services';

const Game = (props) => {
    const repository = new GameRepository();
    const [game, setGame] = useState({
        code: props.match.params.code
    });
    const [loading, setLoading] = useState(true);

    useEffect(() => loadGame(), []);

    function loadGame() {
        repository
            .getGame(props.match.params.code)
            .then(resp => updateGame(resp.data.game))
            .catch(err => {
                props.history.push(Constants.Routes.GAMELIST, { error: err });
            });
    }

    function updateGame(gameData) {
        setGame(gameData);
        setLoading(false);
    }

    return (
        <div>
            <h1>{game.name}</h1>

            <Loader isLoading={loading} />
        </div>
    );
}

export { Game };