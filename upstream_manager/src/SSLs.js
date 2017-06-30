import React from 'react';
import { required, List, Datagrid, Edit, Delete, Create, NumberInput, TextField, EditButton, SimpleForm, TextInput, BooleanInput, LongTextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'


export const SSLList = (props) => (
    <List {...props} filters={<Filter />} title="SSL's List">
        <Datagrid>
            <TextField source="name" />
            <EditButton />
        </Datagrid>
    </List>
);


export const SSLEdit = (props) => (
    <Edit title={<EntityName />}  {...props}>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required} />
            <TextInput source="certificate" defaultValue="" validate={required} />
            <TextInput label="Trust Certificate" source="trstCertificate" defaultValue="" validate={required} />
            <TextInput source="key" defaultValue="" validate={required} />
            <TextInput source="protocols" defaultValue="" validate={required} />
            <TextInput source="dhParam" defaultValue="" validate={required} />
            <LongTextInput source="ciphers" defaultValue="" validate={required} />
            <TextInput label="Session Cache" source="sessionCache" defaultValue="" validate={required} />
            <TextInput label="Session Timeout" source="sessionTimeout" defaultValue="" validate={required} />
            <NumberInput label="Buffer Size" source="bufferSize" defaultValue="" validate={required} />
            <BooleanInput label="Prefer Server Ciphers" source="preferServerCiphers" defaultValue={false} />
            <BooleanInput source="stapling" defaultValue={false} />
            <BooleanInput label="Stapling Verify" source="staplingVerify" defaultValue={false} />
        </SimpleForm>
    </Edit>
);

export const SSLCreate = (props) => (
    <Create {...props }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required} />
            <TextInput source="certificate" defaultValue="" validate={required} />
            <TextInput label="Trust Certificate" source="trstCertificate" defaultValue="" validate={required} />
            <TextInput source="key" defaultValue="" validate={required} />
            <TextInput source="protocols" defaultValue="" validate={required} />
            <TextInput source="dhParam" defaultValue="" validate={required} />
            <LongTextInput source="ciphers" defaultValue="" validate={required} />
            <TextInput label="Session Timeout" source="sessionTimeout" defaultValue="" validate={required} />
            <TextInput source="sessionCache" defaultValue="" validate={required} />
            <NumberInput label="Buffer Size" source="bufferSize" defaultValue="" validate={required} />
            <BooleanInput label="Prefer Server Ciphers" source="preferServerCiphers" defaultValue={false} />
            <BooleanInput source="stapling" defaultValue={false} />
            <BooleanInput label="Stapling Verify" source="staplingVerify" defaultValue={false} />
        </SimpleForm>
    </Create>
);

export const SSLDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);
