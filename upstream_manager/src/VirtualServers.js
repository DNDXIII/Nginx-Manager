import React from 'react';
import { required, NumberField, minValue, maxValue, List, Datagrid, SelectArrayInput, SelectInput, ReferenceInput, Edit, Delete, Create, NumberInput, TextField, EditButton, SimpleForm, TextInput, LongTextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'
import { Field, FieldArray, reduxForm } from 'redux-form'

export const VirtualServerList = (props) => (
    <List title="Virtual Servers List" {...props} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <TextField source="domain" />
            <NumberField source="priority" style={{ textAlign: 'center' }} headerStyle={{ textAlign: 'center' }} />
            <EditButton />
        </Datagrid>
    </List>
);

export const VirtualServerEdit = (props) => (
    <Edit title={<EntityName />}  {...props}>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required} />
            <NumberInput source="priority" defaultValue={0} validate={[required, minValue(0)]} />
            <TextInput source="domain" defaultValue="" validate={required} />
            <NumberInput source="listen" defaultValue="80" validate={[required, minValue(0), maxValue(65535)]} />
            <ReferenceInput label="Application" source="applications" reference="applications" allowEmpty>
                <SelectInput />
            </ReferenceInput>
            <ReferenceInput label="Upstream for the Application" source="applicationsUpstreamId" reference="upstreams" allowEmpty>
                <SelectInput />
            </ReferenceInput>
            <ReferenceInput label="SSL" source="ssl" reference="ssls" allowEmpty>
                <SelectInput optionText="name" />
            </ReferenceInput>
            <ReferenceInput label="Whitelist" source="whitelist" reference="whitelists" allowEmpty>
                <SelectInput optionText="name" />
            </ReferenceInput>
            <LongTextInput source="freeText" />
        </SimpleForm>
    </Edit>
);

const ReferenceSelectInput = (props) => (
    <ReferenceInput {...props} label={props.label} source={props.source} reference={props.reference} allowEmpty>
        <SelectInput />
    </ReferenceInput>
)









//no backend ter um objeto {appid:"ndsnfjads", appupstream:"fdhsufhs"}
//no front end ter um array field que cobre os ReferenceSelectInput












export const VirtualServerCreate = (props) => (
    <Create title="Create new Virtual Server" {...props }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required} />
            <NumberInput source="priority" defaultValue={0} validate={[required, minValue(0)]} />
            <TextInput source="domain" defaultValue="" validate={required} />
            <NumberInput source="listen" defaultValue="80" validate={[required, minValue(0), maxValue(65535)]} />

            <Field
                name={"applications"}
                component={ReferenceSelectInput}
                label="Applications"
                source="applications"
                reference="applications"
                allowEmpty
            /><br />

            <Field
                name={"applicationsUpstreamId"}
                component={ReferenceSelectInput}
                label="Upstream for the Application"
                source="applicationsUpstreamId"
                reference="upstreams"
                allowEmpty
            /><br />

            <ReferenceInput label="SSL" source="ssl" reference="ssls" allowEmpty>
                <SelectInput optionText="name" />
            </ReferenceInput>
            <ReferenceInput label="Whitelist" source="whitelist" reference="whitelists" allowEmpty>
                <SelectInput optionText="name" />
            </ReferenceInput>
            <LongTextInput source="freeText" />
        </SimpleForm>
    </Create>
);

export const VirtualServerDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);


