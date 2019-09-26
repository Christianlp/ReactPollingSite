import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Polls } from './components/polls';
import { Poll } from './components/poll';
import { Results } from './components/results'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/polls' component={Polls} />
                <Route path='/poll/:id' component={Poll} />
                <Route path='/results/:id' component={Results} />
                <Route path='/fetch-data' component={FetchData} />
            </Layout>
        );
    }
}
