import React, { Component } from 'react';
import Option from "./option"

class Question extends Component {
    state = {
        options: []
    }

    constructor(props) {
        super(props);
        //Set state values
        this.state.id = props.id;
        this.state.poll_id = props.poll_id;
        this.state.text = props.text;
    }

    componentDidMount() {
        //Get options for this question
        fetch('api/Options/' + this.state.id)
            .then(response => response.json())
            .then(data => this.setState({
                options: data
            }));
    }

    render() {
        return (
            <div>
                <div className="row">
                    <div className="col-12">
                        <div className="card card-body">
                            <h3>Question: {this.state.text}</h3>
                            {this.state.options.map(option =>
                                <Option key={option.id} id={option.id} question_id={this.state.id} text={option.contents}></Option>
                                )}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Question;