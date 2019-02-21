import React, { useState, useEffect } from 'react';
import { GameRepository } from './GameRepository';
import { Loader } from 'CommonComponents';
import { Constants } from 'Services';

const GameList = (props) => {
    const repository = new GameRepository();
    const [isLoading, setLoading] = useState(true);
    const [gameList, setGameList] = useState([]);

    useEffect(() => {
        repository
            .getGameList()
            .then(resp => onGameListLoaded(resp.data));
    }, []);

    function onGameListLoaded(data) {
        setGameList(data.list);
        setLoading(false);
    }

    function joinGame(game) {
        //Replace with join game logic
        props.history.push(`${Constants.Routes.GAME}/${game.gameCode}`);
    }

    function getDate(date) {
        return new Date(date).toLocaleDateString();
    }

    return (
        <div className="row justify-content-md-center">
            <div className="col-sm-12 col-md-6">
                <h1>Game list</h1>
                <table className="table table-hover">
                    <tbody>
                    {gameList.map(e =>
                        <tr onClick={() => joinGame(e)} key={e.gameCode}>
                            <td>{e.gameName}</td>
                            <td>{e.owner}</td>
                            <td>{getDate(e.createDate)}</td>
                        </tr>
                        )}
                    </tbody>
                </table>
                <Loader isLoading={isLoading} />
            </div>
        </div>
    );
}

export { GameList };