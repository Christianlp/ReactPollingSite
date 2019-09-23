import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';

export class Polls extends Component {
    state = {
        polls: []
    }

    constructor(props) {
        super(props);
        //Set state values
        this.state.loading = true;
        //Load all polls
        fetch('api/Polls')
            .then(response => response.json())
            .then(data => this.setState({
                polls: data,
                loading: false
            }));
    }

    createPollUrl = id => {
        return ("/poll/" + id);
    }

    displayPolls(polls) {
        return (
            <div>
                <div className="row">
                    {polls.map(poll =>
                        <div className="col-4" key={poll.id}>
                            <div className="card p-2">
                                <h4>{poll.name}</h4>
                                <p>{poll.description}</p>
                                <Button tag={Link} color="primary" to={this.createPollUrl(poll.id)}>
                                    Vote in Poll
                                </Button>
                            </div>
                        </div>
                    )}
                </div>
                <br /><br />
                <div style={{ width: '15%', margin: '0px auto 30px auto' }}>
                    <button
                        className="btn btn-primary"
                    >Create New Survey</button>
                </div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.displayPolls(this.state.polls);

        return (
            <div>
                <h1>Surveys</h1>
                <p>This is a list of all created polls that you have permissions to view.</p>
                {contents}
            </div>
        );
    }
}

export default Polls;