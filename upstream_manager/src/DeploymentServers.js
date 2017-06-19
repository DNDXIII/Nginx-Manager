import React from 'react';
import { minValue, maxValue, required, List, BooleanField, Datagrid, Edit, Delete, Create, NumberInput, BooleanInput, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'
import RaisedButton from 'material-ui/RaisedButton';
import { apiUrl } from './App'
import Snackbar from 'material-ui/Snackbar';
import Paper from 'material-ui/Paper';

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
            open: false
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
            else{
                this.setState({ message: "An error ocurred, check console for more information.", open: true });
                resp.text().then((text)=>{console.log(text)})
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

    render() {
        return (
            <div>
                <Edit title={<EntityName />}  {...(this.props) }>
                    <SimpleForm >
                        <TextInput source="name" validate={required} />
                        <TextInput source="address" validate={required} />
                        <NumberInput source="port" validate={[required, minValue(0), maxValue(65535)]} />
                        <BooleanInput label="Active" source="active" />
                        <Paper zDepth={0}>
                            <RaisedButton style={{ margin: 13 }} onTouchTap={() => { this.handleReload(this.props.match.params.id) }} label="Reload Server" />
                            <RaisedButton label="Shutdown Server" onTouchTap={() => { this.handleShutdown(this.props.match.params.id) }} />
                        </Paper>
                    </SimpleForm>
                </Edit>
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

