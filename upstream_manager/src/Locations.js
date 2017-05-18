import React from 'react';
import { required, List,ReferenceField, Datagrid, SelectInput, ReferenceInput,Edit,Delete, Create, TextField, EditButton, SimpleForm, TextInput, LongTextInput} from 'admin-on-rest/lib/mui';
import{EntityName, Filter } from'./Resources'

export const LocationList=(props)=> (
    <List title="Locations List" {...props} filters={<Filter/>}>
        <Datagrid>
            <TextField source="name" />
            <TextField source="uri" />
            <EditButton/>
        </Datagrid>
    </List>
);


export const LocationEdit=(props)=> (
    <Edit title={<EntityName/>}  {...props}>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required } />
            <TextInput source="uri" defaultValue="" validate={required } />
            <ReferenceInput label="Upstream to pass to:" source="pass" reference="upstreams" allowEmpty>
                <SelectInput optionText="name"/>
            </ReferenceInput>
            <SelectInput source="matchType" choices={[
                {id:"=", description:"Exact match (=)"},
                {id:"~", description:"Regex case sensitive (~)"},
                {id:"~*", description:"Regex case insensitive (~*)"},
                {id:"^~", description:"Best non Regex (^~)"},
            ]} optionText="description" optionValue="id" allowEmpty/> 
            <SelectInput source="passType" choices={[
                {id:"proxy_pass"},
                {id:"fastcgi_pass"},
                {id:"uwsgi_pass"},
                {id:"scgi_pass"},
                {id:"memcached_pass"},
            ]} optionText="id" optionValue="id"/>
            <LongTextInput source="freeText" />
        </SimpleForm>
    </Edit>
);


export const LocationCreate=(props)=>(
    <Create title="Create new Virtual Server" {...props }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validate={required } />
            <TextInput source="uri" defaultValue="" validate={required }  />
            <ReferenceInput label="Upstream to pass to:" source="pass" reference="upstreams" allowEmpty>
                <SelectInput optionText="name"/>
            </ReferenceInput>
            <SelectInput source="matchType" choices={[
                {id:"=", description:"Exact match (=)"},
                {id:"~", description:"Case sensitive (~)"},
                {id:"~*", description:"Case insensitive (~*)"},
                {id:"^~", description:"Dunno what to write for this one (^~)"},
            ]} optionText="description" optionValue="id" allowEmpty/> 
            <SelectInput source="passType" choices={[
                {id:"proxy_pass"},
                {id:"fastcgi_pass"},
                {id:"uwsgi_pass"},
                {id:"scgi_pass"},
                {id:"memcached_pass"},
            ]} optionText="id" optionValue="id"/>
            <LongTextInput source="freeText" />
        </SimpleForm>
    </Create>
);

export const LocationDelete=(props)=>(
    <Delete title={<EntityName/>} {...props}/>
);


