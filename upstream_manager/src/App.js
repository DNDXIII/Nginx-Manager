import React from 'react';

import { jsonServerRestClient, Admin, Resource } from 'admin-on-rest';
import { UpList, UpEdit, UpCreate, UpDelete } from './Upstreams';
import {ServerList, ServerCreate, ServerEdit, ServerDelete} from './Servers';
import{ConnectedReferenceArrayField} from './ReferenceArrayField'
import authClient from './authClient';

const App=()=> (
    <Admin authClient={authClient} restClient={jsonServerRestClient('http://localhost:61421/api')} customReducers={ConnectedReferenceArrayField}>
        <Resource name="upstreams" list={UpList} edit={UpEdit} create={UpCreate} remove={UpDelete} />
        <Resource name="proxyTypes"/>
        <Resource name="servers" list={ServerList} edit={ServerEdit} create={ServerCreate} remove={ServerDelete}/>
    </Admin>
);

export default App;
