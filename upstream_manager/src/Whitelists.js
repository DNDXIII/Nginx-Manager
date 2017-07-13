import React from 'react';
import { required, List, Delete, Datagrid, Edit, Create, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources';
import ArrayInput from './ArrayInput';


export const WhitelistEdit = (props) => (
    <Edit title={<EntityName />} {...props} >
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <ArrayInput source="ips"  />
        </SimpleForm>
    </Edit>
);

export const WhitelistCreate = (props) => (
    <Create {...props} >
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <ArrayInput source="ips" />
        </SimpleForm>
    </Create>
);

export const WhitelistList = (props) => (
    <List title="Whitelists List" {...props} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <EditButton />
        </Datagrid>
    </List>
);


export const WhitelistDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);
