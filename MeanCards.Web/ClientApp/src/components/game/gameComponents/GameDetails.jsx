import React, { useState, useEffect } from 'react';
import { Icon } from 'CommonComponents';
import './GameDetails.scss';


const GameDetails = (props) => {
    const [showDetails, setShowDetails] = useState(true);
    const [details, setDetails] = useState(props.details);

    function renderToggleIcon() {
        if (showDetails)
            return <Icon icon="caret-square-up" />;

        return <Icon icon="caret-square-down" />;
    }

    function toggleDetails() {
        setShowDetails(!showDetails);
    }

    function renderDetails() {
        return (
            <div className="game-details-content">
                <div><strong>Name:</strong> {details.name}</div>
                <div><strong>Owner:</strong> {details.owner}</div>
            </div>
        );
    }

    return (
        <div className="game-details border rounded">
            <div className="game-details-header border">
                <button className="btn btn-secondary players-list-header-btn"
                    onClick={() => toggleDetails()}>
                    <strong>Game details</strong> <i>{renderToggleIcon()}</i>
                </button>
            </div>
            {showDetails ? renderDetails() : null}
        </div>
    );
};

export { GameDetails };