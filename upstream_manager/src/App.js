import React from 'react';

import { jsonServerRestClient, Admin, Resource } from 'admin-on-rest';
import { UpList, UpEdit, UpCreate, UpDelete } from './Upstreams';
import {ServerList, ServerCreate, ServerEdit, ServerDelete} from './Servers';
import {VirtualServerCreate, VirtualServerDelete, VirtualServerEdit, VirtualServerList} from './VirtualServers';
import {ProxyList,ProxyEdit, ProxyCreate} from './ProxyTypes';
import {SSLCreate, SSLDelete, SSLEdit, SSLList} from './SSLs';
import {ApplicationCreate, ApplicationDelete, ApplicationEdit, ApplicationList} from './Applications';
import Cloud from 'material-ui/svg-icons/file/cloud-circle';
import Cake from 'material-ui/svg-icons/social/cake';




import authClient from './authClient';

const App=()=> (
    <Admin authClient={authClient} restClient={jsonServerRestClient('http://localhost:3000')}>
        <Resource icon={Cloud} name="virtualservers" list={VirtualServerList} edit={VirtualServerEdit} create={VirtualServerCreate} remove={VirtualServerDelete}/>
        <Resource name="servers" list={ServerList} edit={ServerEdit} create={ServerCreate} remove={ServerDelete}/>
        <Resource name="upstreams" list={UpList} edit={UpEdit} create={UpCreate} remove={UpDelete} />
        <Resource name="applications" list={ApplicationList} edit={ApplicationEdit} create={ApplicationCreate} remove={ApplicationDelete}/>
        <Resource icon={Cake} name="ssls" list={SSLList} edit={SSLEdit} create={SSLCreate} remove={SSLDelete}/>}
        <Resource name="proxytypes" list={ProxyList} edit={ProxyEdit} create={ProxyCreate}/>
    </Admin>
);

export default App;
