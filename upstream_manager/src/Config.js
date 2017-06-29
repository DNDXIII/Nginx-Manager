import React from 'react';
import { Card, CardActions, CardText } from 'material-ui/Card';
import FlatButton from 'material-ui/FlatButton';
import Download from 'material-ui/svg-icons/file/file-download';
import Build from 'material-ui/svg-icons/action/build';
import Upload from 'material-ui/svg-icons/file/file-upload';
import Snackbar from 'material-ui/Snackbar';
import { apiUrl } from './App'


import { ViewTitle } from 'admin-on-rest';

class Config extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            text: "Couldn't get Configuration from server.",
            tested: false,
            message: "",
            open: false
        };
    }

    handleDownload = () => {
        var url = apiUrl.downloadConfig();
        var xmlHttp = new XMLHttpRequest();
        const token = localStorage.getItem('token');
        xmlHttp.open("GET", url, true);
        xmlHttp.setRequestHeader("Authorization", "Bearer " + token)
        xmlHttp.responseType = 'blob'
        xmlHttp.send(null);

        xmlHttp.onload = function (e) {
            if (this.status == 200) {
                var blob = new Blob([this.response], { type: 'text/plain' });
                let a = document.createElement("a");
                a.style = "display: none";
                document.body.appendChild(a);
                let url = window.URL.createObjectURL(blob);
                a.href = url;
                a.download = 'configuration.conf';
                a.click();
                window.URL.revokeObjectURL(url);
            } else {
                alert("Something went wrong.")
            }
        };
    }

    handleTestConfig = () => {
        this.setState({ message: "Testing...", open: true });
        const token = localStorage.getItem('token');
        fetch(apiUrl.testConfig(), {
            headers: new Headers({ 'Authorization': `Bearer ${token}` }),
        })
            .then((resp) => {
                if (resp.ok)
                    this.setState({ message: "Test successful.", open: true, tested: true });
                else
                    resp.text()
                        .then((error) => {
                            console.error(error);
                            this.setState({ message: "Test failed, check console for more information.", open: true });
                        })
            })

    }

    async handleGetConfig() {
        var url = apiUrl.getConfig();
        const token = localStorage.getItem('token');
        const resp = await fetch(url, {
            headers: new Headers({ 'Authorization': `Bearer ${token}` }),
        });
        const text = await resp.text();
        return text;
    }

    handleRequestClose = () => {
        this.setState({
            open: false,
        });
    }

    handleDeploy = () => {
        if (!this.state.tested) {
            this.setState({ message: "File must be successfully tested locally first.", open: true });
            return;
        }

        var socket = new WebSocket(apiUrl.getWebSocket());
        socket.onopen = (e) => {
            this.setState({ message: "Starting deploy.", open: true });
        };

        socket.onopen = (e) => {
            socket.send("ws.DeployConfiguration")
        }

        socket.onclose = function (e) {
            socket.close();
        };

        socket.onmessage = (e) => {
            console.log(e.data);
            this.setState({ message: e.data, open: true });
        };

        socket.onerror = function (e) {
            console.error(e.data);
        };
    }

    async componentDidMount() {
        var t = await this.handleGetConfig();
        this.setState({ text: t });
    }


    render() {
        return (
            <Card>
                <ViewTitle title="Configuration" />
                <CardText>
                    <pre>
                        {this.state.text}
                    </pre>
                </CardText>
                <CardActions>
                    <FlatButton onTouchTap={this.handleDownload} icon={<Download />} label="Download" />
                    <FlatButton onTouchTap={this.handleTestConfig} icon={<Build />} label="Test Locally" />
                    <FlatButton onTouchTap={this.handleDeploy} icon={<Upload />} label="Deploy" />
                </CardActions>
                <Snackbar
                    open={this.state.open}
                    message={this.state.message}
                    autoHideDuration={10000}
                    onRequestClose={this.handleRequestClose}
                />
            </Card>
        );
    }
}

export default Config;

