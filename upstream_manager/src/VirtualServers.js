import React from 'react';
import { List,Filter, Datagrid, Edit,Delete, Create,NumberInput, TextField, EditButton, SimpleForm, TextInput, LongTextInput} from 'admin-on-rest';
import{EntityName} from'./Resources'

export const VirtualServerList=(props)=> (
    <List {...props} filters={<VirtualServerFilter/>}>
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

export const VirtualServerCreate=(props)=>(
    <Create {...props }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <NumberInput source="listen" defaultValue="80" validation={{ required: true, min:0, max:65535 }}/>
            {/* TODO Locations  https://github.com/marmelab/admin-on-rest/blob/master/docs/Inputs.md*/}
            <TextInput source="name" defaultValue="" validation={{ required: true }} />

            <LongTextInput source= "freetext" defaultValue=""/> 
 
        </SimpleForm>
    </Create>
);

export const VirtualServerDelete=(props)=>(
    <Delete title={<EntityName/>} {...props}/>
);

const VirtualServerFilter=(props)=> (
    <Filter {...props}>
        <TextInput label="Search" source="q" alwaysOn />
    </Filter>
);

