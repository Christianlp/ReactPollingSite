import React, { Component } from 'react';

class Option extends Component {
    state = {
        clicked: false
    }

    constructor(props) {
        super(props);
        //Set state values
        this.state.id = props.id;
        this.state.question_id = props.question_id;
        this.state.text = props.text;
    }

    handleOptionChange = changeEvent => {
        this.setState({
            selectedOption: changeEvent.target.value
        });
    }

    render() {
        return (
            <div>
                <label>
                    <input className="mr-3" type="radio" value="{this.state.id}"
                        checked={this.state.clicked}
                        onChange={this.handleOptionChange} />
                    {this.state.text}
                </label>
            </div>
        );
    }
}

export default Option;