import React, { Component } from 'react';
import { withAuth } from './auth/AuthComponents';
import { AuthHttpService } from 'Services';

class Counter extends Component {
  static displayName = Counter.name;
  state = {
    currentCount: 0,
    step: 1
  };

  componentDidMount() {
    var http = new AuthHttpService();

    http.get('values/2')
      .then(resp => {
        this.setState({
          step: resp.data
        });
      });
  }

  incrementCounter = () => {
    this.setState({
      currentCount: this.state.currentCount + this.state.step
    });
  };

  render() {
    return (
      <div>
        <h1>Counter</h1>

        <p>This is a simple example of a React component.</p>
        <p>Increment step value: <strong>{this.state.step}</strong></p>

        <p>Current count: <strong>{this.state.currentCount}</strong></p>

        <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
      </div>
    );
  }
}

const CounterWithAuth = withAuth(Counter);
export { CounterWithAuth as Counter };