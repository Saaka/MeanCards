import React, { useState, useEffect } from 'react';
import { Loader } from 'CommonComponents';

const Game = (props) => {
    const [game, setGame] = useState({
        number: 1,
        code: props.match.params.code
    });
    const [loading, setLoading] = useState(true);

    useEffect(() => setLoading(false), []);

    function updateGame() {
        setGame(prev => ({
            ...game,
            number: prev.number + 1 
        }));
    }

    return (
        <div>
            <h1>Game {game.code}</h1>
            <h2>{game.number}</h2>
            <button className="btn btn-primary"
                onClick={updateGame}>Update</button>
            <Loader isLoading={loading} />
        </div>
    );
}

export { Game };