import React from 'react';
import { minValue, maxValue, required, List, Datagrid, Edit, Delete, Create, NumberInput, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
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
            <NumberInput source="port" validate={[required, minValue(0), maxValue(65535)]} />
            <NumberInput source="maxFails" validate={[required, minValue(0), maxValue(65535)]} />
            <NumberInput source="failTimeout" validate={[required, minValue(0), maxValue(65535)]} />
        </SimpleForm>
    </Edit>
);

export const ServerCreate = (servers) => (
    <Create {...servers }>
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <TextInput source="address" validate={required} />
            <NumberInput source="port" defaultValue={80} validate={[required, minValue(0), maxValue(65535)]} />
            <NumberInput source="maxFails" defaultValue={1} validate={[required, minValue(0), maxValue(65535)]} />
            <NumberInput source="failTimeout" defaultValue={10} validate={[required, minValue(0), maxValue(65535)]} />
        </SimpleForm>
    </Create>
);

export const ServerDelete = (servers) => (
    <Delete title={<EntityName />} {...servers} />
);
