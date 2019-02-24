import React, { useState, useEffect } from 'react';
import { Avatar, Icon } from 'CommonComponents';
import './PlayersList.scss';

const PlayersList = (props) => {
    const [players, setPlayers] = useState(props.players);
    const [showPlayersList, setShowPlayersList] = useState(true);

    useEffect(() => {
        console.log(`Players count: ${players.length}`);
    }, []);

    function togglePlayersList() {
        setShowPlayersList(!showPlayersList);
    }

    function renderList() {
        if (showPlayersList)
            return players
                .map(p =>
                    <li className="list-group-item players-list-item" key={p.playerId}>
                        <Avatar imageUrl={p.avatar} /> <span>{p.displayName} <span className="badge badge-secondary">{p.points}</span></span>
                    </li>);

        return null;
    }

    function renderToggleIcon() {
        if (showPlayersList)
            return <Icon icon="caret-square-up" />;

        return <Icon icon="caret-square-down" />;
    }

    return (
        <ul className="players-list list-group-flush border rounded">
            <li className="list-group-item players-list-header" key="0">
                <button className="btn btn-secondary players-list-header-btn"
                    onClick={() => togglePlayersList()}>
                    <strong>Players list</strong> <i>{renderToggleIcon()}</i>
                </button>
            </li>
            {renderList()}
        </ul>
    );
};

export { PlayersList };