import React, { Component } from 'react';

class Question extends Component {
    state = {
        selectedOption: "",
        options: []
    }

    constructor(props) {
        super(props);
        //Set state values
        this.state.id = props.id;
        this.state.poll_id = props.poll_id;
        this.state.text = props.text;
    }

    handleOptionChange = changeEvent => {
        this.setState({ saving: true });
        setTimeout(function () { this.setState({ saving: false }) }.bind(this), 1000);
        this.setState({
            selectedOption: changeEvent.target.value
        });

    }

    componentDidMount() {
        //Get options for this question
        fetch('api/Options/' + this.state.id)
            .then(response => response.json())
            .then(data => this.setState({
                options: data
            }));
    }

    showSavedState() {
        console.log(this.state.saving);
        if (this.state.saving !== undefined) {
            let contents = this.state.saving
                ? <p className="text-secondary"><em>Saving...</em></p>
                : <p className="text-success">Saved!</p>;
            return contents;
        }
        else {
            return <p></p>
        }
    }

    render() {
        return (
            <div>
                <div className="row">
                    <div className="col-12">
                        <div className="card card-body">
                            <div className="row">
                                <div className="col-10">
                                    <h3>Question: {this.state.text}</h3>
                                </div>
                                <div className="col-2"> 
                                    {this.showSavedState()}
                                </div>
                            </div>
                            {this.state.options.map(option =>
                                <div key={option.id}>
                                    <label>
                                        <input className="mr-3" type="radio" value={option.id}
                                            checked={this.state.selectedOption === option.id} 
                                            onChange={this.handleOptionChange} />
                                        {option.contents}
                                    </label>
                                </div>    
                            )}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Question;