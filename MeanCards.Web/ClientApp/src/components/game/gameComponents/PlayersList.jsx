import React, {useState, useEffect} from 'react';
import { Avatar } from 'CommonComponents';

const PlayersList = (props) => {
    const [players, setPlayers] = useState(props.players);

    return (
        <div>
            <ul className="list-unstyled">
                {players.map(p => <li className="" key={p.playerId}><Avatar imageUrl={p.avatar} /> <span>{p.displayName}</span></li>)}
            </ul>
        </div>
    );
};

export { PlayersList };