import React from 'react';
import { required, List, Delete, Datagrid, Edit, Create, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources';
import ArrayInput from './ArrayInput';


export const BlacklistEdit = (props) => (
    <Edit title={<EntityName />} {...props} >
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <ArrayInput />
        </SimpleForm>
    </Edit>
);

export const BlacklistCreate = (props) => (
    <Create {...props} >
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <ArrayInput />
        </SimpleForm>
    </Create>
);

export const BlacklistList = (props) => (
    <List {...props} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <EditButton />
        </Datagrid>
    </List>
);


export const BlacklistDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);