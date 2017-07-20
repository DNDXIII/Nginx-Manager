import React from 'react';
import { required, List, ReferenceInput, Datagrid, SelectInput, LongTextInput, Edit, Delete, Create, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'
import { Card, CardHeader } from 'material-ui/Card';
import { Field, FieldArray, reduxForm } from 'redux-form'
import { default as TextFieldMUI } from 'material-ui/TextField';
import RaisedButton from 'material-ui/RaisedButton';
import Divider from 'material-ui/Divider';

export const ApplicationList = (props) => (
    <List {...props} filters={<Filter />}>
        <Datagrid>
            <TextField source="name" />
            <EditButton />
        </Datagrid>
    </List>
);


export const ApplicationEdit = (props) => (
    <Edit title={<EntityName />}  {...props}>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required} />
            </SimpleForm>
    </Edit>
);

export const ApplicationCreate = (props) => (
    <Create {...props }>
        <SimpleForm >
            <Field name="name" label="Name" component={TextInput} validate={required} />
            <Field name="protocol" component={SelectInput} choices={[
                { id: 'http://', name: 'HTTP' },
                { id: 'https://', name: 'HTTPS' },
            ]} optionValue="id" allowEmpty validate={required} label="Protocol" /><br />
            <ul style={{
                listStyle: "none",
                paddingLeft: 13,
                paddingBottom: 13,
                paddingTop: 13
            }}>


                <h3>Locations</h3>

                <FieldArray name="locations" component={(locations) =>
                    <div style={{ marginLeft: 13 }}>
                        {
                            locations.fields.map((loc, index) =>
                                <li key={index}>
                                    <Field
                                        name={`${loc}.uri`}
                                        component={TextInput}
                                        label="URI"
                                    /><br />
                                    <Field name={`${loc}.passtype`} component={SelectInput} choices={[
                                        { id: "proxy_pass" },
                                        { id: "fastcgi_pass" },
                                        { id: "uwsgi_pass" },
                                        { id: "scgi_pass" },
                                        { id: "memcached_pass" },
                                    ]} optionText="id" optionValue="id" allowEmpty label="Pass Type" /><br />


                                    <Field name={`${loc}.matchType`} component={SelectInput} choices={[
                                        { id: "=", description: "Exact match (=)" },
                                        { id: "~", description: "Regex case sensitive (~)" },
                                        { id: "~*", description: "Regex case insensitive (~*)" },
                                        { id: "^~", description: "Best non Regex (^~)" },
                                    ]} optionText="description" optionValue="id" allowEmpty label="Match Type" /><br />

                                    <Field name={`${loc}.freeText`} component={LongTextInput} allowEmpty label="Free Text" /><br />

                                    <RaisedButton secondary={true} style={{ marginTop: 13, marginBottom: 13 }} label="remove" onTouchTap={() => locations.fields.remove(index)} />
                                    <Divider />

                                </li>
                            )}
                        <RaisedButton primary={true} style={{ marginTop: 26, marginBottom: 13, }} label="Add Location" onTouchTap={() => locations.fields.push({})} />
                    </div>

                } />
            </ul>

        </SimpleForm>
    </Create>
);


export const ApplicationDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);
