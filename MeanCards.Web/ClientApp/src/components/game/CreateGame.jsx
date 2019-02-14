import React, { Component } from 'react';
import { LoaderButton, Select } from 'CommonComponents';

export class CreateGame extends Component {
    static displayName = CreateGame.name;

    state = {
        isLoading: false,
        name: "",
        pointsLimit: 10,
        languageId: 0,
        languages: []
    };

    componentDidMount = () => {

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
                                onChange={this.handleChange}
                                required>
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
                        <div className="form-group">
                            <label htmlFor="language">Language</label>
                            <Select id="language"
                                name="languageId"
                                values={this.state.languages}
                                onChange={this.handleChange}
                            ></Select>
                        </div>
                        <LoaderButton type="submit" className="btn btn-primary" text="Create" isLoading={this.state.isLoading} />
                    </form>
                </div>
            </div>
        );
    }
}
