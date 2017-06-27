import React from 'react';
import { minValue, maxValue, required, List, BooleanField, Datagrid, Edit, Delete, Create, NumberInput, BooleanInput, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'
import RaisedButton from 'material-ui/RaisedButton';
import { apiUrl } from './App'
import Snackbar from 'material-ui/Snackbar';
import Terminal from './Terminal'

export const DeploymentServerList = (props) => (
    <List title="Deployment Servers List" {...props} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <TextField source="address" />
            <TextField source="port" />
            <BooleanField headerStyle={{ textAlign: 'center' }} source="active" />
            <EditButton />
        </Datagrid>
    </List>
);



export class DeploymentServerEdit extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            message: "",
            open: false,
            usertext: [],
            alltext: [],
            socket: null
        };
    }


    handleReload(id) {
        const url = apiUrl.nginxReload(id);
        const token = localStorage.getItem('token');
        fetch(url, {
            method: 'POST',
            body: null,
            headers: new Headers({ 'Authorization': `Bearer ${token}` }),

        }).then((resp) => {
            if (resp.ok)
                this.setState({ message: "Service has been reloaded successfully.", open: true });
            else {
                this.setState({ message: "An error ocurred, check console for more information.", open: true });
                resp.text().then((text) => { console.error(text) })
            }
        })
    }

    handleStart(id) {
        const url = apiUrl.nginxStart(id)
        const token = localStorage.getItem('token');
        fetch(url, {
            method: 'POST',
            body: null,
            headers: new Headers({ 'Authorization': `Bearer ${token}` }),

        }).then((resp) => {
            if (resp.ok)
                this.setState({ message: "Service has been started successfully.", open: true });
            else {
                this.setState({ message: "An error ocurred, check console for more information.", open: true });
                resp.text().then((text) => { console.error(text) })
            }
        })
    }


    handleShutdown(id) {
        const url = apiUrl.nginxShutdown(id);
        const token = localStorage.getItem('token');
        fetch(url, {
            method: 'POST',
            body: null,
            headers: new Headers({ 'Authorization': `Bearer ${token}` }),

        }).then((resp) => {
            if (resp.ok)
                this.setState({ message: "Service has been shutdown successfully.", open: true });
            else
                alert("not ok")
        })
    }

    handleRequestClose = () => {
        this.setState({
            open: false,
        });
    }

    handleConnect = () => {
        var s = new WebSocket(apiUrl.getWebSocket());

        this.setState({
            socket: s
        })

        s.onopen = e => {
            console.log("Connected");
            s.send(this.props.match.params.id);//to receive th header
        };

        s.onclose = (e) => {
            s.close();
            this.setState({
                socket: null,
                alltext: [],
                usertext: []
            })
            console.log("Disconnected");
        };

        s.onmessage = (e) => {
            var newText = this.state.alltext;
            newText.push(e.data);
            this.setState({
                alltext: newText,
            })
            var div = document.getElementById("textBox");
            div.scrollTop = div.scrollHeight - div.clientHeight;
            console.log(e.data)
        };

        s.onerror = function (e) {
            console.error(e.data);
        };
    }

    handleDisconnect = () => {
        this.state.socket.close();
        this.setState({ socket: null });
    }

    handleWriteLine = (line) => {
        var newUserText = this.state.usertext;
        var newAllText = this.state.alltext;
        var socket = this.state.socket;
        debugger;
        if (line === "" || !line) {
            newAllText.push(line);
            this.setState({
                alltext: newAllText
            });
        }
        else {
            newUserText.push(line)
            this.setState({
                usertext: newUserText
            });
            socket.send(line);
        }
        var div = document.getElementById("textBox");
        div.scrollTop = div.scrollHeight - div.clientHeight;
    }

    getText = () => {
        var s = "";
        for (var i = 0; i < this.state.alltext.length; i++)
            s = s + this.state.alltext[i] + "\n";

        return s;
    }

    render() {
        return (
            <div>
                <Edit title={<EntityName />}  {...(this.props) }>
                    <SimpleForm >
                        <TextInput source="name" validate={required} />
                        <TextInput source="address" validate={required} />
                        <NumberInput source="port" validate={[required, minValue(0), maxValue(65535)]} />
                        <BooleanInput label="Active" source="active" />
                        <div>
                            <RaisedButton style={{ marginRight: 13, marginBottom: 20 }} onTouchTap={() => { this.handleReload(this.props.match.params.id) }} label="Reload Server" />
                            <RaisedButton style={{ marginRight: 13 }} label="Shutdown Server" onTouchTap={() => { this.handleShutdown(this.props.match.params.id) }} />
                            <RaisedButton style={{ marginRight: 13 }} label="Start Server" onTouchTap={() => { this.handleStart(this.props.match.params.id) }} />

                            {!this.state.socket == null ? null : <RaisedButton onTouchTap={this.handleConnect} label="Connect by SSH" />}

                        </div>
                    </SimpleForm>
                </Edit>
                {this.state.socket == null ? null : <Terminal handleDisconnect={this.handleDisconnect} usertext={this.state.usertext} alltext={this.getText} handleWriteLine={this.handleWriteLine} />}

                <Snackbar
                    open={this.state.open}
                    message={this.state.message}
                    autoHideDuration={3000}
                    onRequestClose={this.handleRequestClose}
                />
            </div>
        );
    }
}



export const DeploymentServerCreate = (props) => (
    <Create {...props }>
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <TextInput source="address" validate={required} />
            <NumberInput source="port" defaultValue={80} validate={[required, minValue(0), maxValue(65535)]} />
            <BooleanInput label="Active" source="active" />
        </SimpleForm>
    </Create>
);

export const DeploymentServerDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);

