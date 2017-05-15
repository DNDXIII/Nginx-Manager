import React from 'react';
import { List, Datagrid,CheckboxGroupInput, SelectInput, ReferenceInput,Edit,Delete, Create,NumberInput, TextField, EditButton, SimpleForm, TextInput, LongTextInput} from 'admin-on-rest/lib/mui';
import{EntityName, Filter } from'./Resources'

export const VirtualServerList=(props)=> (
    <List title="Virtual Servers List" {...props} filters={<Filter/>}>
        <Datagrid>
            <TextField source="name" />
            <EditButton/>
        </Datagrid>
    </List>
);

export const VirtualServerEdit=(props)=> (
    <Edit title={<EntityName/>}  {...props}>
       <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <NumberInput source="listen" defaultValue="80" validation={{ required: true, min:0, max:65535 }}/>
            <ReferenceInput label="Application" source="application" reference="applications" allowEmpty>
                <SelectInput optionText="name"/>
            </ReferenceInput>
            <ReferenceInput label="SSL" source="ssl" reference="ssls" allowEmpty>
                <SelectInput optionText="name"/>
            </ReferenceInput>
            <ReferenceInput label="Locations" source="locations" reference="locations" allowEmpty validation={{required:true}}>
                <CheckboxGroupInput optionText="name"/>
            </ReferenceInput>
            <LongTextInput source="freetext" defaultValue=""/> 
        </SimpleForm>
    </Edit>
);


export const VirtualServerCreate=(props)=>(
    <Create title="Create new Virtual Server" {...props }>
         <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <NumberInput source="listen" defaultValue="80" validation={{ required: true, min:0, max:65535 }}/>
            <ReferenceInput label="Application" source="application" reference="applications" allowEmpty>
                <SelectInput optionText="name"/>
            </ReferenceInput>
            <ReferenceInput label="SSL" source="ssl" reference="ssls" allowEmpty>
                <SelectInput optionText="name"/>
            </ReferenceInput>
            <ReferenceInput label="Locations" source="locations" reference="locations" allowEmpty validation={{required:true}}>
                <CheckboxGroupInput optionText="name"/>
            </ReferenceInput>
            <LongTextInput source="freetext" defaultValue=""/> 
        </SimpleForm>
    </Create>
);

export const VirtualServerDelete=(props)=>(
    <Delete title={<EntityName/>} {...props}/>
);


