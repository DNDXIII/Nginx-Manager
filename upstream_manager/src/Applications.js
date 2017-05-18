import React from 'react';
import {required, List,ReferenceInput,SelectInput, Datagrid,CheckboxGroupInput, Edit,Delete, Create, TextField, EditButton, SimpleForm, TextInput  } from 'admin-on-rest/lib/mui';
import{EntityName, Filter} from'./Resources'


export const ApplicationList=(props)=> (
    <List {...props} filters={<Filter/>}>
        <Datagrid>
            <TextField source="name" />
            <EditButton/>
        </Datagrid>
    </List>
);


export const ApplicationEdit=(props)=> (
    <Edit title={<EntityName/>}  {...props}>
        <SimpleForm >
            <TextInput source="name" defaultValue=""validate={required } />
            <ReferenceInput label="Override Upstream" allowEmpty source="upstreamId" reference="upstreams" >
                <SelectInput optionText="name" />
            </ReferenceInput>
            <ReferenceInput label="Locations" source="locations" reference="locations" allowEmpty validate={required}>
                <CheckboxGroupInput optionText="name"/>
            </ReferenceInput>
        </SimpleForm>
    </Edit>
);

export const ApplicationCreate=(props)=>(
    <Create {...props }>
         <SimpleForm >
            <TextInput source="name" defaultValue=""validate={required } />
            <ReferenceInput label="Override Upstream" allowEmpty source="upstreamId" reference="upstreams" >
                <SelectInput optionText="name" />
            </ReferenceInput>
            <ReferenceInput label="Locations" source="locations" reference="locations" allowEmpty validate={required}>
                <CheckboxGroupInput optionText="name"/>
            </ReferenceInput>
        </SimpleForm>
    </Create>
);

export const ApplicationDelete=(props)=>(
    <Delete title={<EntityName/>} {...props}/>
);
