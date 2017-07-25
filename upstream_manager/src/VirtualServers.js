import React from 'react';
import { required, NumberField, minValue, maxValue, List, Datagrid, SelectArrayInput, SelectInput, ReferenceInput, Edit, Delete, Create, NumberInput, TextField, EditButton, SimpleForm, TextInput, LongTextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'
import { Field, FieldArray, reduxForm } from 'redux-form'
import RaisedButton from 'material-ui/RaisedButton';
import Divider from 'material-ui/Divider';


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
            <RenderApplications/>
        </SimpleForm>
    </Edit>
);

const ReferenceSelectInput = (props) => (
    <ReferenceInput {...props} label={props.label} source={props.source} reference={props.reference} allowEmpty>
        <SelectInput />
    </ReferenceInput>
)

export const VirtualServerCreate = (props) => (
    <Create title="Create new Virtual Server" {...props }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required} />
            <NumberInput source="priority" defaultValue={0} validate={[required, minValue(0)]} />
            <TextInput source="domain" defaultValue="" validate={required} />
            <NumberInput source="listen" defaultValue="80" validate={[required, minValue(0), maxValue(65535)]} />
            <ReferenceInput label="SSL" source="ssl" reference="ssls" allowEmpty>
                <SelectInput optionText="name" />
            </ReferenceInput>
            <ReferenceInput label="Whitelist" source="whitelist" reference="whitelists" allowEmpty>
                <SelectInput optionText="name" />
            </ReferenceInput>
            <LongTextInput source="freeText" />
            <RenderApplications />

        </SimpleForm>
    </Create>
);

const RenderApplications = (props) => (
    <FieldArray name="applications" component={(apps) => (
        <div style={{ marginLeft: 13 }}>
            <h3>Applications</h3>
            {
                apps.fields.map((app, index) =>
                    <li style={{
                        listStyleType: "none"
                    }} key={index}>
                        <Field key="_1"
                            {...props}
                            name={`${app}.applicationId`}
                            component={ReferenceSelectInput}
                            label="Applications"
                            source="applicationId"
                            reference="applications"
                            allowEmpty
                            validate={required}
                        /><br />

                        <Field key="_2"
                            {...props}
                            name={`${app}.upstreamToPass`}
                            component={ReferenceSelectInput}
                            label="Upstream for the Application"
                            reference="upstreams"
                            allowEmpty
                            validate={required}
                        /><br />
                        <RaisedButton secondary={true} style={{ marginTop: 13, marginBottom: 13 }} label="remove" onTouchTap={() => apps.fields.remove(index)} />
                        <Divider />

                    </li>
                )
            }
            <RaisedButton primary={true} style={{ marginTop: 26, marginBottom: 13, }} label="Add Application" onTouchTap={() => apps.fields.push({})} />

        </div >
    )} />
)

export const VirtualServerDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);


