import React from 'react';
import {Filter as F,TextInput} from 'admin-on-rest/lib/mui';


export const EntityName=({ record })=> {
    return <span>Eddit{record ? `: ${record.name}` : ''}</span>;
};

export const Filter=(props)=> (
    <F {...props}>
        <TextInput label="Search" source="q" alwaysOn />
    </F>
);