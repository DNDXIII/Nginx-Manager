import React from 'react';
import {required, CheckboxGroupInput, LongTextInput, List, Datagrid, Edit, Delete, ReferenceField, Create, TextField, EditButton, ReferenceInput, SelectInput, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources';


export const UpList = (upstreams) => (
    <List {...upstreams} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <ReferenceField label="Proxy Type" source="proxyTypeId" reference="proxytypes" >
                <TextField source="name" />
            </ReferenceField>
            <EditButton />
        </Datagrid>
    </List>
);

export const UpEdit = (upstreams) => (
    <Edit title={<EntityName />} {...upstreams} >
        <SimpleForm>
            <TextInput source="name" validate={required}/>
            <SelectInput source="protocol" optionValue="id" validate={required} choices={[
                { id: 'http://', name: 'HTTP' },
                { id: 'https://', name: 'HTTPS' },
            ]} />
            <ReferenceInput label="Proxy Type" allowEmpty source="proxyTypeId" reference="proxytypes" validate={required}  >
                <SelectInput optionText="name" />
            </ReferenceInput>
            <ReferenceInput label="Servers" source="serverIds" reference="servers" allowEmpty validate={required}>
                <CheckboxGroupInput optionText="name" />
            </ReferenceInput>
            <LongTextInput source="freeText" />
        </SimpleForm>
    </Edit>
);


export const UpCreate = (upstreams) => (
    <Create {...upstreams }>
        <SimpleForm>
            <TextInput source="name" validate={required}/>
            <SelectInput source="protocol" validate={required} optionValue="id" choices={[
                { id: 'http://', name: 'HTTP' },
                { id: 'https://', name: 'HTTPS' },
            ]} />
            <ReferenceInput label="Proxy Type" allowEmpty source="proxyTypeId" reference="proxytypes" validate={required}  >
                <SelectInput optionText="name" />
            </ReferenceInput>
            <ReferenceInput label="Servers" source="serverIds" reference="servers" allowEmpty validate={required}>
                <CheckboxGroupInput optionText="name" />
            </ReferenceInput>
            <LongTextInput source="freeText"/>
        </SimpleForm>
    </Create>
);



export const UpDelete = (upstreams) => (
    <Delete title={<EntityName />} {...upstreams} />
);
