import React, { useState, useEffect } from 'react';
import { GameRepository } from './GameRepository';
import { Loader } from 'CommonComponents';
import { Alert } from 'reactstrap';
import { Constants } from 'Services';

const GameList = (props) => {
    const repository = new GameRepository();
    const [isLoading, setLoading] = useState(true);
    const [gameList, setGameList] = useState([]);
    const [error, setError] = useState("");
    const [showError, setShowError] = useState(false);

    useEffect(() => {
        repository
            .getGameList()
            .then(resp => onGameListLoaded(resp.data));
    }, []);

    useEffect(() => {
        if(props.location.state && props.location.state.error) {
            toggleError(true, props.location.state.error);
        }
    }, [])

    function onGameListLoaded(data) {
        setGameList(data.list);
        setLoading(false);
    }

    function joinGame(game) {
        if (game.alreadyJoined)
            redirectToGame(game.gameCode);
        else {
            repository
                .joinGame(game.gameCode)
                .then(resp => redirectToGame(game.gameCode))
                .catch(err => toggleError(true, err));
        }
    }

    function redirectToGame(gameCode) {
        props.history.push(`${Constants.Routes.GAME}/${gameCode}`);
    }

    function getDate(date) {
        return new Date(date).toLocaleDateString();
    }

    function toggleError(show, error) {
        setError(error);
        setShowError(show);
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
                <Alert color="danger" isOpen={showError} toggle={() => toggleError(false)}>{error}</Alert>
                <Loader isLoading={isLoading} />
            </div>
        </div>
    );
}

export { GameList };