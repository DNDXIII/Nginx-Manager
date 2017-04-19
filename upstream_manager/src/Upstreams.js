import React from 'react';
import { SingleFieldList, ChipField, List,Filter,NumberInput, TabbedForm,FormTab, Datagrid, Edit,Delete,ReferenceField, Create, TextField, EditButton,  ReferenceInput, SelectInput, SimpleForm, TextInput} from 'admin-on-rest/lib/mui';
import{EntityName} from'./Resources';
import{ReferenceArrayField} from './ReferenceArrayField'


const UpFilter=(upstreams)=> (
    <Filter {...upstreams}>
        <TextInput label="Search" source="q" alwaysOn />
    </Filter>
);


export const UpList=(upstreams)=> (
    <List {...upstreams} filters={<UpFilter/>}>
        <Datagrid>
            <TextField source="name"/>
            <ReferenceField label="Proxy Type" source="proxyTypeId" reference="proxyTypes" >
                <TextField source="type"/>
            </ReferenceField>
            <EditButton/>
        </Datagrid>
    </List>
);

export const UpEdit=(upstreams)=> (
    <Edit title={<EntityName/>} {...upstreams} >
       <TabbedForm>
            <FormTab label="comments">
              <ReferenceArrayField label="Upstream's Servers" reference="servers" source="serversId">
                <SingleFieldList>
                   <ChipField source="name" />
                 </SingleFieldList> 
             </ReferenceArrayField>
            </FormTab>
        </TabbedForm>
    </Edit>
);


export const UpCreate=(upstreams)=>(
    <Create {...upstreams }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <ReferenceInput label="Proxy Type" defaultValue={1} source="proxyTypeId" reference="proxyTypes" >
                <SelectInput optionText="type"/>
            </ReferenceInput>
            <NumberInput label='Maximum Number of Fails' source='max_fails' defaultValue={1} validation={{min:0}} />
            <NumberInput label='Fail Timeout (seconds)' source='fail_timeout' defaultValue={10} validation={{min:0}} />

        </SimpleForm>
    </Create>
);



export const UpDelete=(upstreams)=>(
    <Delete title={<EntityName/>} {...upstreams}/>
);



/*
 <TabbedForm>
          <FormTab label="Upstream's Settings">
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <ReferenceInput label="Proxy Type" defaultValue={1} source="proxyTypeId" reference="proxyTypes">
              <SelectInput optionText="type" />
            </ReferenceInput>
          </FormTab>
          <FormTab label="Upstream's Servers">
            <ReferenceArrayField label="Upstream's Servers" reference="servers" source="serversId">
                <Datagrid>
                   <TextField source="name" />
                   <TextField source="ip"/>
                   <TextField source="port"/>
                 </Datagrid> 
             </ReferenceArrayField>
          </FormTab>
        </TabbedForm>
        */