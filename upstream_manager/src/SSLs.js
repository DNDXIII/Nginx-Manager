import React from 'react';
import { List, Datagrid, Edit,Delete, Create,NumberInput, TextField, EditButton, SimpleForm, TextInput,BooleanInput,LongTextInput  } from 'admin-on-rest/lib/mui';
import{EntityName, Filter} from'./Resources'


export const SSLList=(props)=> (
    <List {...props} filters={<Filter/>}>
        <Datagrid>
            <TextField source="name" />
            <EditButton/>
        </Datagrid>
    </List>
);


export const SSLEdit=(props)=> (
    <Edit title={<EntityName/>}  {...props}>
        <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <TextInput source="certificate" defaultValue="" validation={{ required: true }}/>
            <TextInput label="Trust Certificate" source="trstCertificate" defaultValue="" validation={{ required: true }}/>
            <TextInput source="key" defaultValue="" validation={{ required: true}}/>
            <TextInput source="protocols" defaultValue="" validation={{ required: true}}/>
            <TextInput source="dhParam" defaultValue="" validation={{ required: true}}/>
            <LongTextInput source="ciphers" defaultValue="" validation={{ required: true}}/>
            <TextInput label="Session Timeout" source="sessionTimeout" defaultValue="" validation={{ required: true}}/>
            <NumberInput label="Buffer Size" source="bufferSize" defaultValue="" validation={{ required: true}}/>
            <BooleanInput label="Prefer Server Ciphers" source="preferServerCiphers"defaultValue={false} />
            <BooleanInput source="stapling" defaultValue={false} />
            <BooleanInput label="Stapling Verify" source="staplingVerify"defaultValue={false} />
        </SimpleForm>
    </Edit>
);

export const SSLCreate=(props)=>(
    <Create {...props }>
         <SimpleForm >
            <TextInput source="name" defaultValue="" validation={{ required: true }} />
            <TextInput source="certificate" defaultValue="" validation={{ required: true }}/>
            <TextInput label="Trust Certificate" source="trstCertificate" defaultValue="" validation={{ required: true }}/>
            <TextInput source="key" defaultValue="" validation={{ required: true}}/>
            <TextInput source="protocols" defaultValue="" validation={{ required: true}}/>
            <TextInput source="dhParam" defaultValue="" validation={{ required: true}}/>
            <LongTextInput source="ciphers" defaultValue="" validation={{ required: true}}/>
            <TextInput label="Session Timeout" source="sessionTimeout" defaultValue="" validation={{ required: true}}/>
            <NumberInput label="Buffer Size" source="bufferSize" defaultValue="" validation={{ required: true}}/>
            <BooleanInput label="Prefer Server Ciphers" source="preferServerCiphers"defaultValue={false} />
            <BooleanInput source="stapling" defaultValue={false} />
            <BooleanInput label="Stapling Verify" source="staplingVerify"defaultValue={false} />
        </SimpleForm>
    </Create>
);

export const SSLDelete=(props)=>(
    <Delete title={<EntityName/>} {...props}/>
);
