import React from 'react';
import { List,Datagrid, Edit, Create, TextField, EditButton,SimpleForm, TextInput} from 'admin-on-rest/lib/mui';
import{EntityName, Filter} from'./Resources';


export const ProxyEdit=(props)=> (
    <Edit title={<EntityName/>} {...props} >
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <TextInput source="description" defaultValue="" validation={{ required: true }} />
            <TextInput label="Value" source="proxyValue" defaultValue=""  />
        </SimpleForm>
    </Edit>
);

export const ProxyCreate=(props)=> (
    <Create title="Create a new Proxy Type" {...props} >
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <TextInput source="description" defaultValue="" validation={{ required: true }} />
            <TextInput label="Value" source="proxyValue" defaultValue=""  />
        </SimpleForm>
    </Create>
);

export const ProxyList=(props)=> (
    <List title="Proxy Types List" {...props} filters={<Filter/>}>
        <Datagrid>
            <TextField source="name" />
            <EditButton/>
        </Datagrid>
    </List>
);