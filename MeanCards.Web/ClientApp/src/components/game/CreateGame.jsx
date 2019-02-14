import React, { Component } from 'react';
import { LoaderButton, Select } from 'CommonComponents';

export class CreateGame extends Component {
    static displayName = CreateGame.name;
    validations = {
        minPoints: 5,
        maxPoints: 20,
        gameNameLen: 50
    };
    state = {
        isLoading: false,
        isSubmitted: false,
        isValid: true,
        name: "Game",
        pointsLimit: 10,
        adultContent: false,
        languageId: 1,
        languages: [{ id: 1, name: "Polski" }]
    };

    componentDidMount = () => {
        // Load languages
    };

    handleSubmit = (event) => {
        event.preventDefault();
        var formIsValid = event.target.checkValidity();

        this.setState({
            isSubmitted: true,
            isValid: formIsValid
        }, () => {
            if (!formIsValid) return;

            console.log("saving");
        });
    };

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState(
            {
                isValid: true,
                [name]: value
            }
        )
    };

    handleToggleChange = (e) => {
        const { name } = e.target;
        this.setState(prevState => ({
            isValid: true,
            [name]: !prevState[name]
        }));
    };

    formClass = () => {
        if (this.state.isSubmitted)
            return "was-validated";

        return "";
    };

    render() {
        return (
            <div className="row justify-content-md-center">
                <div className="col-sm-12 col-md-6">
                    <h1>Create game</h1>
                    <form name="createGame" onSubmit={this.handleSubmit} noValidate className={this.formClass()}>
                        <div className="form-group">
                            <label htmlFor="gameName">Name</label>
                            <input type="text"
                                className="form-control"
                                id="gameName"
                                name="name"
                                maxLength={this.validations.gameNameLen}
                                value={this.state.name}
                                onChange={this.handleChange}
                                required>
                            </input>
                            <div className="invalid-feedback">
                                Game name is required (maxinum name length is {this.validations.gameNameLen})
                            </div>
                        </div>
                        <div className="form-group">
                            <label htmlFor="gamePointsLimit">Points limit</label>
                            <input type="number"
                                className="form-control"
                                id="gamePointsLimit"
                                name="pointsLimit"
                                min={this.validations.minPoints}
                                max={this.validations.maxPoints}
                                value={this.state.pointsLimit}
                                onChange={this.handleChange}>
                            </input>
                            <div className="invalid-feedback">
                                Points limit should be between {this.validations.minPoints} and {this.validations.maxPoints}
                            </div>
                        </div>
                        <div className="form-group">
                            <label htmlFor="language">Language</label>
                            <Select className="custom-select"
                                id="language"
                                name="languageId"
                                values={this.state.languages}
                                onChange={this.handleChange}
                                disabled
                                required />
                        </div>
                        <div className="form-group">
                            <div className="form-check">
                                <input className="form-check-input"
                                    type="checkbox"
                                    id="adultContent"
                                    name="adultContent"
                                    checked={this.state.adultContent}
                                    onChange={this.handleToggleChange} />
                                <label className="form-check-label"
                                    htmlFor="adultContent">
                                    Include adult content
                            </label>
                            </div>
                        </div>
                        <LoaderButton type="submit"
                            className="btn btn-primary"
                            text="Create"
                            isLoading={this.state.isLoading}
                            disabled={!this.state.isValid} />
                    </form>
                </div>
            </div>
        );
    }
}
