import React from 'react';
import { Card, CardActions, CardText } from 'material-ui/Card';
import FlatButton from 'material-ui/FlatButton';
import Download from 'material-ui/svg-icons/file/file-download';
import Build from 'material-ui/svg-icons/action/build';
import Upload from 'material-ui/svg-icons/file/file-upload';
import Snackbar from 'material-ui/Snackbar';


import { ViewTitle } from 'admin-on-rest';

class Config extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            text: "Couldn't get Config from server.",
            tested: false,
            message: "",
            open: false
        };
    }

    handleDownload = () => {
        var url = "http://localhost:5000/api/config/download";
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open("GET", url, true);
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
        fetch("http://localhost:5000/api/config/test")
            .then((resp) => {
                if (resp.ok)
                    this.setState({ message: "Test successful.", open: true, tested: true });
                else
                    resp.text()
                        .then((error) => {
                            this.setState({ message: error, open: true });
                        })
            })

    }

    async handleGetConfig() {
        var url = "http://localhost:5000/api/config";
        const resp = await fetch(url);
        const text = await resp.text();
        return text;
    }

    handleRequestClose = () => {
        this.setState({
            open: false,
        });
    }

    handleDeploy = () => {//todo
        if (!this.state.tested){
            this.setState({ message: "File must be successfully tested locally first.", open: true });
            return;
        }

        var socket = new WebSocket("ws://localhost:5000/api");
        socket.onopen = e => {
            this.setState({ message: "Starting deploy.", open: true });
        };

        socket.onclose = function (e) {
            console.log("socket closed", e);
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
                    {this.state.text}
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





function handleDeployConfig() {
    var url = "http://localhost:5000/api/config/deploy";
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);

    xmlHttp.onload = function (e) {
        if (this.status == 200)
            alert("File has been deployed successfully:\n" + this.responseText);
        else if (this.status == 500)
            alert("The file deployment has failed:\n " + this.responseText);
        else
            alert("Something went wrong.");
    }
}




