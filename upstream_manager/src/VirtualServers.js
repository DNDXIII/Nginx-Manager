import React from 'react';
import { List, Datagrid, Edit,Delete, Create,NumberInput, TextField, EditButton, SimpleForm, TextInput, LongTextInput} from 'admin-on-rest/lib/mui';
import{EntityName, Filter } from'./Resources'
import Toggle from 'material-ui/Toggle'

export const VirtualServerList=(props)=> (
    <List title="Virtual Servers List" {...props} filters={<Filter/>}>
        <Datagrid>
            <TextField source="name" />
            <EditButton/>
        </Datagrid>
    </List>
);

export const VirtualServerEdit=(props)=> (
    <Edit title={<EntityName/>}  {...props}>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <TextInput source="address" defaultValue="" validation={{ required: true }}/>
            <NumberInput source="port" defaultValue="" validation={{ required: true , min:0, max:65535}}/>
        </SimpleForm>
    </Edit>
);

class SSLToggle extends React.Component {  
    constructor(props) {
        super(props);
        this.state = {Toggled: false};
      }

    handleToggle() {
        this.setState({Toggled: !this.state.Toggled});
    }

    render() {
    return (    
            <Toggle
                label="SSL"
                defaultToggled={this.state.Toggled}
                onToggle={this.handleToggle.bind(this)}
                labelPosition="right"
                toggled={this.state.Toggled}
            />   
        );
    }

}


export const VirtualServerCreate=(props)=>(
    <Create title="Create new Virtual Server" {...props }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <NumberInput source="listen" defaultValue="80" validation={{ required: true, min:0, max:65535 }}/>
            {/* TODO Locations  https://github.com/marmelab/admin-on-rest/blob/master/docs/Inputs.md*/}
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <LongTextInput source="freetext" defaultValue=""/> 

            <SSLToggle/>

 
        </SimpleForm>
    </Create>
);

export const VirtualServerDelete=(props)=>(
    <Delete title={<EntityName/>} {...props}/>
);


