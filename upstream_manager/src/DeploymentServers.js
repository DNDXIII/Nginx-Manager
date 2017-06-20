import React from 'react';
import { minValue, maxValue, required, List, BooleanField, Datagrid, Edit, Delete, Create, NumberInput, BooleanInput, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'
import RaisedButton from 'material-ui/RaisedButton';
import { apiUrl } from './App'
import Snackbar from 'material-ui/Snackbar';
import Paper from 'material-ui/Paper';
import Terminal from './Terminal'

export const DeploymentServerList = (props) => (
    <List {...props} filters={<Filter />}>
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
        const url = apiUrl.nginxReload(id)
        fetch(url, {
            method: 'POST',
            body: null,
        }).then((resp) => {
            if (resp.ok)
                this.setState({ message: "Service has been reloaded successfully.", open: true });
            else {
                this.setState({ message: "An error ocurred, check console for more information.", open: true });
                resp.text().then((text) => { console.log(text) })
            }
        })
    }

    handleStart(id) {
        const url = apiUrl.nginxStart(id)
        fetch(url, {
            method: 'POST',
            body: null,
        }).then((resp) => {
            if (resp.ok)
                this.setState({ message: "Service has been started successfully.", open: true });
            else {
                this.setState({ message: "An error ocurred, check console for more information.", open: true });
                resp.text().then((text) => { console.log(text) })
            }
        })
    }


    handleShutdown(id) {
        const url = apiUrl.nginxShutdown(id);
        fetch(url, {
            method: 'POST',
            body: null,
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
            console.log("connected");
        };

        s.onclose = (e)=> {
            s.close();
            this.setState({
                socket:null
            })
            console.log("disconnected");
        };

        s.onmessage = (e) => {
            var newText = this.state.alltext;
            newText.push(e.data);
            this.setState({
                alltext: newText,
            })
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
        newAllText.push(line);
        newUserText.push(line)
        this.setState({
            usertext: newUserText,
            alltext: newAllText
        });
        socket.send(line);
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

