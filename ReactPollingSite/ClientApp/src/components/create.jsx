import React, { Component } from 'react';

export class Create extends Component {
    static displayName = Create.name;
    
    createNewPollUrl = () => {
        return ("/api/Poll");
    }

    render() {
        return (
            <div>
                <h3> Create new Poll</h3>
                <form action={this.createNewPollUrl()} method="post">
                    <div style={{ width: '35%', margin: '0px auto 30px auto' }}>
                        <input className="form-control" type="text" id="Name" name="Name" placeholder="Poll Name" />
                        <input className="form-control" type="text" id="Description" name="Description" placeholder="Description" />
                        <input className="form-control" type="number" id="Count" name="Count" min="1" max="10" placeholder="Question Count" />
                    </div>
                    <div style={{ width: '15%', margin: '0px auto 30px auto' }}>
                        <button type="submit" className="btn btn-primary">
                            Create
                        </button>
                    </div>
                </form>
            </div>
        );
    }
}

export default Create;