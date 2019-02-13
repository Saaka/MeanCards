import React, { Component } from 'react';
import { LoaderButton } from 'CommonComponents';

export class CreateGame extends Component {
    static displayName = CreateGame.name;

    state = {
        isLoading: false,
        name: "",
        pointsLimit: 10
    };

    handleSubmit = (event) => {
        event.preventDefault();

        console.log(this.state);
    };

    handleChange = (e) => {
        this.setState(
            {
                [e.target.name]: e.target.value
            }
        )
    };

    render() {
        return (
            <div className="row justify-content-md-center">
                <div className="col-sm-12 col-md-6">
                    <h1>Create game</h1>
                    <form onSubmit={this.handleSubmit}>
                        <div className="form-group">
                            <label htmlFor="gameName">Name</label>
                            <input type="text"
                                className="form-control"
                                id="gameName"
                                name="name"
                                value={this.state.name}
                                onChange={this.handleChange}>
                            </input>
                        </div>
                        <div className="form-group">
                            <label htmlFor="gamePointsLimit">Points limit</label>
                            <input type="number"
                                className="form-control"
                                id="gamePointsLimit"
                                name="pointsLimit"
                                value={this.state.pointsLimit}
                                onChange={this.handleChange}>
                            </input>
                        </div>
                        <LoaderButton type="submit" className="btn btn-primary" text="Create" isLoading={this.state.isLoading} />
                    </form>
                </div>
            </div>
        );
    }
}
