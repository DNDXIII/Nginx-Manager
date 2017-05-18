import React from 'react';
import {required, List,TextInput,Delete, Datagrid, Edit, Create, TextField, EditButton, SimpleForm, LongTextInput } from 'admin-on-rest/lib/mui';
import { EntityName } from './Resources'


export const GeneralConfigList = (props) => (
    <List {...props} title="General Config" >
        <Datagrid>
            <TextField source="name" />
            <EditButton />
        </Datagrid>
    </List>
);

export const GeneralConfigEdit = (props) => (
    <Edit title={<EntityName />}  {...props}>
        <SimpleForm >
            <TextInput source="name" validate={required}/>
            <LongTextInput source="text" validate={required} />
        </SimpleForm>
    </Edit>
);

export const GeneralConfigCreate = (props) => (
    <Create {...props }>
        <SimpleForm >
            <LongTextInput source="text" validate={required} />
        </SimpleForm>
    </Create>
);

export const GeneralConfigDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);

