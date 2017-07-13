import React from 'react';
import { required, List, Datagrid, Edit, Delete, Create, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'


export const ServerList = (servers) => (
    <List {...servers} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <TextField source="address" />
            <TextField source="port" />
            <EditButton />
        </Datagrid>
    </List>
);

export const ServerEdit = (servers) => (
    <Edit title={<EntityName />}  {...servers}>
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <TextInput source="address" validate={required} />
        </SimpleForm>
    </Edit>
);

export const ServerCreate = (servers) => (
    <Create {...servers }>
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <TextInput source="address" validate={required} />
        </SimpleForm>
    </Create>
);

export const ServerDelete = (servers) => (
    <Delete title={<EntityName />} {...servers} />
);
