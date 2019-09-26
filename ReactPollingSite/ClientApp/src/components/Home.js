import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>Hello!</h1>
                <p>Welcome to my first ReactJS website, a poll creation and voting site.</p>
                <p>This website is built using:</p>
                <ul>
                    <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                    <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
                    <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
                </ul>
                <p>Currently implemented features include:</p>
                <ul>
                    <li><strong>Poll Navigation</strong>. View the polls that have been created by users by clicking <em>Polls</em>.</li>
                    <li><strong>Voting Simulation</strong>. Click <em>Vote in Poll</em> on a poll card to view the poll simulate live voting.</li>
                    <li><strong>Poll Results</strong>. You can also view fictitious results for the various polls on the site by clicking <em>Results</em> on any poll card.</li>
                </ul>
                <p>This React website was built based on the <a href='https://get.asp.net/'>ASP.NET Core 2.2</a> ReactJS template. The <code>react-sweet-progress</code> node_module was added in addition to the default template package to aid in the creation of the poll results chart visualization.</p>
            </div>
        );
    }
}
