import React from 'react';
import { List, Datagrid, Edit,Delete, Create,NumberInput, TextField, EditButton, SimpleForm, TextInput} from 'admin-on-rest/lib/mui';
import{EntityName, Filter} from'./Resources'


export const ServerList=(servers)=> (
    <List {...servers} filters={<Filter/>}>
        <Datagrid>
            <TextField source="name" />
            <TextField source="address"/>
            <TextField source="port"/>
            <EditButton/>
        </Datagrid>
    </List>
);

export const ServerEdit=(servers)=> (
    <Edit title={<EntityName/>}  {...servers}>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <TextInput source="address" defaultValue="" validation={{ required: true }}/>
            <NumberInput source="port" defaultValue="" validation={{ required: true , min:0, max:65535}}/>
        </SimpleForm>
    </Edit>
);

export const ServerCreate=(servers)=>(
    <Create {...servers }>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <TextInput source="address" defaultValue="" validation={{ required: true }}/>
            <NumberInput source="port" defaultValue="" validation={{ required: true, min:0, max:65535 }}/>

        </SimpleForm>
    </Create>
);

export const ServerDelete=(servers)=>(
    <Delete title={<EntityName/>} {...servers}/>
);
