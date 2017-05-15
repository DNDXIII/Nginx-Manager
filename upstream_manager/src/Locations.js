import React from 'react';
import { List,ReferenceField, Datagrid, SelectInput, ReferenceInput,Edit,Delete, Create, TextField, EditButton, SimpleForm, TextInput, LongTextInput} from 'admin-on-rest/lib/mui';
import{EntityName, Filter } from'./Resources'

export const LocationList=(props)=> (
    <List title="Locations List" {...props} filters={<Filter/>}>
        <Datagrid>
            <TextField source="name" />
            <ReferenceField label="Passes to:" source="pass" reference="upstreams">
                <TextField source="name"/>
            </ReferenceField>

            <EditButton/>
        </Datagrid>
    </List>
);


export const LocationEdit=(props)=> (
    <Edit title={<EntityName/>}  {...props}>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <TextInput source="uri" defaultValue="" validation={{ required: true }} />
            <ReferenceInput label="Upstream to pass to:" source="pass" reference="upstreams">
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
            <LongTextInput source="freeText" defaultValue=""  />
        </SimpleForm>
    </Edit>
);


export const LocationCreate=(props)=>(
    <Create title="Create new Virtual Server" {...props }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <TextInput source="uri" defaultValue="" validation={{ required: true }}  />
            <ReferenceInput label="Upstream to pass to:" source="pass" reference="upstreams" validation={{ required: true }} allowEmpty>
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
            <LongTextInput source="freeText" defaultValue=""  />
        </SimpleForm>
    </Create>
);

export const LocationDelete=(props)=>(
    <Delete title={<EntityName/>} {...props}/>
);


