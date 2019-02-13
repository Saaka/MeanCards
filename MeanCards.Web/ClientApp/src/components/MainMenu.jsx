import React, { Component } from 'react';
import { ListGroup, ListGroupItem, Row } from 'reactstrap';
import { Link } from 'react-router-dom';

export class MainMenu extends Component {
    render() {
        return (
            <Row className="justify-content-md-center">
                <div className="col-sm-12 col-md-6">
                    <h1>Menu</h1>
                    <div className="list-group">
                        <ListGroupItem tag={Link} to="/">Home</ListGroupItem>
                        <ListGroupItem className="list-group-item-action" tag={Link} to="/counter">Counter</ListGroupItem>
                        <Link className="list-group-item list-group-item-action" to="/countdown">Countdown</Link>
                        <Link className="list-group-item list-group-item-action" to="/createGame">Create game</Link>
                    </div>
                </div>
            </Row>
        );
    }
}