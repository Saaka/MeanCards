import React, { Component } from 'react';
import { ListGroup, ListGroupItem } from 'reactstrap';
import { Link } from 'react-router-dom';

export class MainMenu extends Component {


    render() {
        return (
            <ListGroup>
                <ListGroupItem tag={Link} to="/">Home</ListGroupItem>
                <ListGroupItem tag={Link} to="/counter">Counter</ListGroupItem>
                <ListGroupItem tag={Link} to="/countdown">Countdown</ListGroupItem>
            </ListGroup>
        );
    }
}