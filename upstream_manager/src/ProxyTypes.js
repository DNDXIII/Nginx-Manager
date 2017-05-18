import React from 'react';
import { required, List, Datagrid, Edit, Create, TextField, EditButton, SimpleForm, TextInput, LongTextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources';


export const ProxyEdit = (props) => (
    <Edit title={<EntityName />} {...props} >
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required } />
            <LongTextInput source="description" defaultValue="" validate={required } />
            <TextInput label="Value" source="proxyValue" defaultValue="" />
        </SimpleForm>
    </Edit>
);

export const ProxyCreate = (props) => (
    <Create title="Create a new Proxy Type" {...props} >
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required } />
            <LongTextInput source="description" defaultValue="" validate={required } />
            <TextInput label="Value" source="proxyValue" defaultValue="" />
        </SimpleForm>
    </Create>
);

export const ProxyList = (props) => (
    <List title="Proxy Types List" {...props} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <EditButton />
        </Datagrid>
    </List>
);