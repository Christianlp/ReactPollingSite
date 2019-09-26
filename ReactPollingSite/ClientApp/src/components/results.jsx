import React, { Component } from 'react';
import { Progress } from 'react-sweet-progress';
import "react-sweet-progress/lib/style.css";

export class Results extends Component {
    state = {
        questions: []
    }

    constructor(props) {
        super();
        this.state.id = props.match.params.id;
    }

    componentDidMount() {
        //Get questions for this poll
        fetch('api/Results/' + this.state.id)
            .then(response => response.json())
            .then(data => this.setState({
                questions: data
            }));
    }

    render() {
        return (
            <div>
                <div className="row">
                    {this.state.questions.map(question =>
                        <div className="col-12" key={question.question_id}>
                            <div className="card p-2">
                                <h5>{question.content}</h5>
                                {question.options.map(option =>
                                    <div key={option.option_id}>
                                        <p>{option.content}</p>
                                        <div className="pr-5">
                                            <Progress percent={option.voteCount / 10} width="80%" status="active" />
                                        </div>
                                    </div>
                                )}
                            </div>
                        </div>

                    )}
                </div>
            </div>
        );
    }
}

export default Results;