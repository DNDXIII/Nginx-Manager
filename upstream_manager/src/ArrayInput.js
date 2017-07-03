import React from 'react'
import { FieldArray,Field } from 'redux-form'
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';

const renderTextField = ({ input, label, meta: { touched, error }, ...custom }) => (
    <TextField hintText={label}
        floatingLabelText={label}
        errorText={touched && error}
        {...input}
        {...custom}
    />
)

const RenderMap = ({ fields, meta: { error } }) => (
    <ul style={{
        listStyle: "none",
        paddingLeft: 0
    }}>
        {fields.map((ip, index) =>
            <li style={{ listStyleType: "none" }} key={index}>
                <Field
                    name={ip}
                    type="text"
                    component={renderTextField}
                    label={`Ip #${index + 1}`} />
                <RaisedButton style={{ marginLeft: 13 }} label="remove" onTouchTap={() => fields.remove(index)} />

            </li>
        )}
        <RaisedButton label="Add Ip" onTouchTap={() => fields.push("Ip")} />

    </ul>
)

const FieldArraysForm = (props) => {
    return (
        <span>
            <FieldArray name={props.source} component={RenderMap} />
        </span>

    )
}

export default FieldArraysForm
