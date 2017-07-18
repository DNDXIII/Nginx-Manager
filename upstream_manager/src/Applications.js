import React from 'react';
import { required, List, ReferenceInput, Datagrid, SelectArrayInput, Edit, Delete, Create, TextField, EditButton, SimpleForm, TextInput } from 'admin-on-rest/lib/mui';
import { EntityName, Filter } from './Resources'
import { Card, CardHeader } from 'material-ui/Card';
import { Field, FieldArray, reduxForm } from 'redux-form'
import { default as TextFieldMUI } from 'material-ui/TextField';
import RaisedButton from 'material-ui/RaisedButton';


const renderTextField = ({ input, label, meta: { touched, error }, ...custom }) => (
    <TextFieldMUI
        hintText={label}
        floatingLabelText={label}
        errorText={touched && error}
        {...input}
        {...custom}
    />
);

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
            <Locations />
        </SimpleForm>
    </Edit>
);

export const ApplicationCreate = (props) => (
    <Create {...props }>
        <SimpleForm >
            <Locations />

        </SimpleForm>
    </Create>
);


export class Locations extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expanded: false,
        };
    }

    handleExpandChange = (expanded) => {
        this.setState({ expanded: expanded });
    }

        render(){
            return(
                <div>
                    <Field style={{paddingLeft: 13}} name="name" label="Name" component={renderTextField}/>
                    <ul style={{
                        listStyle: "none",
                        paddingLeft: 13,
                        paddingBottom: 13,
                        paddingTop: 13
                    }}>
                        <h3>Locations</h3>
                       
                        <FieldArray name="locations" component={(locations) =>
                            <div>
                                {
                                    locations.fields.map((loc, index)=>
                                    <li  key={index}>
                                        <Field 
                                            style={{marginLeft:13}}
                                            name={`${loc}.name`}
                                            component={renderTextField}
                                            label={`Name`} /><br/>
                                        <Field
                                            style={{marginLeft:13}}
                                            name={`${loc}.uri`}
                                            component={renderTextField}
                                            label={`Uri`} /><br/>
                                        <RaisedButton secondary={true} style={{ marginLeft: 13 }} label="remove" onTouchTap={() => locations.fields.remove(index)} />

                                    </li>
                                )}
                                <RaisedButton primary={true} style={{ marginLeft: 13, marginTop:13, marginBottom:13, }}  label="Add Location" onTouchTap={() => locations.fields.push({})} />
                            </div>

                        }/>
                    </ul>
                </div>
            )
        }
}

export const ApplicationDelete = (props) => (
    <Delete title={<EntityName />} {...props} />
);
 /*
  {
                fields.map((loc, index) => (
                    <li style={{ listStyleType: "none" }} key={index}>
                        <Field
                            name={fields.get(index).ola}
                            type="text"
                            component={renderTextField}
                            label={`Location #${index + 1}`} />
                        <RaisedButton style={{ marginLeft: 13 }} label="remove" onTouchTap={() => fields.remove(index)} />

                    </li>
                ))}
            <RaisedButton label="Add Location" onTouchTap={() => fields.push({ola:"ola"})} />
            */