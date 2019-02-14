import React, { Component } from 'react';
import { AuthHttpService } from 'Services';

class Counter extends Component {
  static displayName = Counter.name;
  state = {
    currentCount: 0,
    step: 1
  };

  componentDidMount = () => {
    var http = new AuthHttpService();

    http.get('values/2')
      .then(resp => {
        this.setState({
          step: resp.data
        });
      });
  };

  incrementCounter = () => {
    this.setState(prevState => ({
      currentCount: prevState.currentCount + prevState.step
    }));
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

export { Counter };