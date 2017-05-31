import React from 'react';
import { minValue, maxValue, required, List,BooleanField, Datagrid, Edit, Delete, Create, NumberInput,BooleanInput, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'


export const DeploymentServerList = (props) => (
    <List {...props} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <TextField source="address" />
            <TextField source="port" />
            <BooleanField headerStyle={{ textAlign: 'center' }} source="active" />
            <EditButton />
        </Datagrid>
    </List>
);

export const DeploymentServerEdit = (props) => (
    <Edit title={<EntityName />}  {...props}>
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <TextInput source="address" validate={required} />
            <NumberInput source="port" validate={[required, minValue(0), maxValue(65535)]} />
            <BooleanInput label="Active" source="active" />
        </SimpleForm>
    </Edit>
);

export const DeploymentServerCreate = (props) => (
    <Create {...props }>
        <SimpleForm >
            <TextInput source="name" validate={required} />
            <TextInput source="address" validate={required} />
            <NumberInput source="port" defaultValue={80} validate={[required, minValue(0), maxValue(65535)]} />
            <BooleanInput label="Active" source="active" />
        </SimpleForm>
    </Create>
);

export const DeploymentServerDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);
