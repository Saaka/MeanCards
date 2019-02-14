import React, { Component } from 'react';

export class Countdown extends Component {
    static displayName = Countdown.name;

    state = { currentCount: 10 }

    decrementCounter = () => {
        if (this.state.currentCount === 0) return;

        this.setState(prevState => ({
            currentCount: prevState.currentCount - 1
        }));
    }

    render() {
        return (
            <div>
                <h1>Countdown</h1>

                <p>This is a simple example of a React component.</p>

                <p>Current count: <strong>{this.state.currentCount}</strong></p>

                <button className="btn btn-primary" onClick={this.decrementCounter}>Decrement</button>
            </div>
        );
    }
}
