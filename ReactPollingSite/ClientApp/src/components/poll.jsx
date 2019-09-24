import React, { Component } from 'react';
import Question from './question';

export class Poll extends Component {
    state = {
        questions: []
    }

    constructor(props) {
        super();
        this.state.id = props.match.params.id;
    }

    componentDidMount() {
        //Get poll information
        fetch('api/Poll/' + this.state.id)
            .then(response => response.json())
            .then(data => this.setState({
                title: data.name,
                description: data.description,
                author: data.creator
            }));

        //Get questions for this poll
        fetch('api/Questions/' + this.state.id)
            .then(response => response.json())
            .then(data => this.setState({
                questions: data
            }));
    }

    render() {
        return (
            <div>
                <h3>{this.state.title}</h3>

                <form onSubmit={this.handleFormSubmit}>
                    {this.state.questions.map(question => <Question key={question.id} id={question.id} poll_id={this.state.id} text={question.contents}>
                    </Question>)
                    }
                    <br/>
                    <button className="btn btn-default" type="submit">Save</button>
                </form>

            </div>
        );
    }
}

export default Poll;