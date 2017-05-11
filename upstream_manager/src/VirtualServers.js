import React from 'react';
import { List, Datagrid, SelectInput, ReferenceInput,Edit,Delete, Create,NumberInput, TextField, EditButton, SimpleForm, TextInput, LongTextInput} from 'admin-on-rest/lib/mui';
import{EntityName, Filter } from'./Resources'
import RaisedButton from 'material-ui/RaisedButton'

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

const Location=()=> (
    <h1>Boas</h1>
);

const AddLocations = React.createClass( {  
    getInitialState:function(){
        return {locations:[], i:0};
    },

    handleAddLocation:function() {
        var locs = this.state.locations;
        locs.push(<Location key={this.state.i}/>);
        this.setState({locations:locs,i:(this.state.i+1)});
    },

    render: function() {
    return (
            <div>
                {this.state.locations}
                <RaisedButton label="Add Location" primary={true} onTouchTap={this.handleAddLocation}/>
            </div>
        );
    }

});


export const VirtualServerCreate=(props)=>(
    <Create title="Create new Virtual Server" {...props }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <NumberInput source="listen" defaultValue="80" validation={{ required: true, min:0, max:65535 }}/>
            {/* TODO Locations  https://github.com/marmelab/admin-on-rest/blob/master/docs/Inputs.md*/}
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <LongTextInput source="freetext" defaultValue=""/> 
            <ReferenceInput label="Application" source="application" reference="applications" allowEmpty>
                <SelectInput optionText="name"/>
            </ReferenceInput>
            <ReferenceInput label="SSL" source="ssl" reference="ssls" allowEmpty>
                <SelectInput optionText="name"/>
            </ReferenceInput>

            <AddLocations/>
 
        </SimpleForm>
    </Create>
);

export const VirtualServerDelete=(props)=>(
    <Delete title={<EntityName/>} {...props}/>
);


