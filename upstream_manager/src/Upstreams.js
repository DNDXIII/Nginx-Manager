import React from 'react';
import { CheckboxGroupInput, LongTextInput,  List, Datagrid, Edit,Delete,ReferenceField, Create, TextField, EditButton,  ReferenceInput, SelectInput, SimpleForm, TextInput} from 'admin-on-rest/lib/mui';
import{EntityName, Filter} from'./Resources';


export const UpList=(upstreams)=> (
    <List {...upstreams} filters={<Filter/>}>
        <Datagrid>
            <TextField source="name"/>
            <ReferenceField label="Proxy Type" source="proxyTypeId" reference="proxytypes" >
                <TextField source="name"/>
            </ReferenceField>
            <EditButton/>
        </Datagrid>
    </List>
);

export const UpEdit=(upstreams)=> (
    <Edit title={<EntityName/>} {...upstreams} >
        <SimpleForm>
            <TextInput source="name"/>
            <ReferenceInput label="Proxy Type" allowEmpty source="proxyTypeId" reference="proxytypes" validation={{required:true}}  >
                <SelectInput optionText="name" />
            </ReferenceInput>
            <ReferenceInput label="Servers" source="serverIds" reference="servers" allowEmpty validation={{required:true}}>
                <CheckboxGroupInput optionText="name"/>
            </ReferenceInput>
            <LongTextInput source="freeText" defaultValue=""/> 
        </SimpleForm>
    </Edit>
);


export const UpCreate=(upstreams)=>(
    <Create {...upstreams }>
       <SimpleForm>
            <TextInput source="name"/>
            <ReferenceInput label="Proxy Type" allowEmpty source="proxyTypeId" reference="proxytypes" validation={{required:true}}  >
                <SelectInput optionText="name" />
            </ReferenceInput>
            <ReferenceInput label="Servers" source="serverIds" reference="servers" allowEmpty validation={{required:true}}>
                <CheckboxGroupInput optionText="name"/>
            </ReferenceInput>
            <LongTextInput source="freeText" defaultValue=""/> 
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
            <ReferenceInput label="Proxy Type" defaultValue={1} source="proxyTypeId" reference="proxytypes">
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