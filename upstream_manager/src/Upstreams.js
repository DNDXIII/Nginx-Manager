import React from 'react';
import {minValue, maxValue, NumberInput, required, CheckboxGroupInput, LongTextInput, List, Datagrid, Edit, Delete, Create, TextField, EditButton, ReferenceInput, SelectInput, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources';


export const UpList = (upstreams) => (
    <List {...upstreams} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <EditButton />
        </Datagrid>
    </List>
);

export const UpEdit = (upstreams) => (
    <Edit title={<EntityName />} {...upstreams} >
        <SimpleForm>
            <TextInput source="name" validate={required}/>
            <NumberInput source="port" validate={[required, minValue(0), maxValue(65535)]} />
            <NumberInput source="maxFails" label="Max Fails" validate={[required, minValue(0), maxValue(65535)]} />
            <NumberInput source="failTimeout" label="Fail Timeout" validate={[required, minValue(0), maxValue(65535)]} />
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
            <NumberInput source="port" defaultValue={80} validate={[required, minValue(0), maxValue(65535)]} />
            <NumberInput source="maxFails" label="Max Fails" defaultValue={1} validate={[required, minValue(0), maxValue(65535)]} />
            <NumberInput source="failTimeout" label="Fail Timeout" defaultValue={10} validate={[required, minValue(0), maxValue(65535)]} />
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
