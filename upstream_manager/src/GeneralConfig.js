import React from 'react';
import {required, List, Datagrid, Edit, EditButton, SimpleForm, LongTextInput } from 'admin-on-rest/lib/mui';
import { EntityName } from './Resources'
import LargeTextField from './LargeTextField'


export const GeneralConfigList = (props) => (
    <List {...props} title="General Config" >
        <Datagrid>
            <LargeTextField source="text" />
            <EditButton />
        </Datagrid>
    </List>
);

export const GeneralConfigEdit = (props) => (
    <Edit title={<EntityName />}  {...props}>
        <SimpleForm >
            <LongTextInput label = "Text. The {{config}} will be replaced will be reaplaced by the configuration." source="text" validate={required} />
        </SimpleForm>
    </Edit>
);



