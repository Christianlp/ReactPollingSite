import React, { Component } from 'react';

export class Create extends Component {
    static displayName = Create.name;

    constructor(props) {
        super(props);
        this.state = { poll_id: "", questions: {} }
    }
}