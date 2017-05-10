import React from 'react';

import { jsonServerRestClient, Admin, Resource } from 'admin-on-rest';
import { UpList, UpEdit, UpCreate, UpDelete } from './Upstreams';
import {ServerList, ServerCreate, ServerEdit, ServerDelete} from './Servers';
import {VirtualServerCreate, VirtualServerDelete, VirtualServerEdit, VirtualServerList} from './VirtualServers';
import authClient from './authClient';

const App=()=> (
    <Admin authClient={authClient} restClient={jsonServerRestClient('http://localhost:5000/api')}>
        <Resource name="upstreams" list={UpList} edit={UpEdit} create={UpCreate} remove={UpDelete} />
        <Resource name="servers" list={ServerList} edit={ServerEdit} create={ServerCreate} remove={ServerDelete}/>
        <Resource name="virtualservers" list={VirtualServerList} edit={VirtualServerEdit} create={VirtualServerCreate} remove={VirtualServerDelete}/>
        <Resource name="proxytypes"/>
    </Admin>
);

export default App;
