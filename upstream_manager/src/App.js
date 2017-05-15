import React from 'react';

import { jsonServerRestClient, Admin, Resource } from 'admin-on-rest';
import { UpList, UpEdit, UpCreate, UpDelete } from './Upstreams';
import {ServerList, ServerCreate, ServerEdit, ServerDelete} from './Servers';
import {VirtualServerCreate, VirtualServerDelete, VirtualServerEdit, VirtualServerList} from './VirtualServers';
import {ProxyList,ProxyEdit, ProxyCreate} from './ProxyTypes';
import {SSLCreate, SSLDelete, SSLEdit, SSLList} from './SSLs';
import {ApplicationCreate, ApplicationDelete, ApplicationEdit, ApplicationList} from './Applications';
import {LocationCreate, LocationDelete, LocationEdit, LocationList} from './Locations';

import Menu from './Menu';
import CustomRoutes from './CustomRoutes';


import authClient from './authClient';

const App=()=> (
    <Admin menu={Menu} customRoutes={CustomRoutes} authClient={authClient} restClient={jsonServerRestClient('http://localhost:50116//api')}>
        <Resource name="virtualservers" list={VirtualServerList} edit={VirtualServerEdit} create={VirtualServerCreate} remove={VirtualServerDelete}/>
        <Resource name="servers" list={ServerList} edit={ServerEdit} create={ServerCreate} remove={ServerDelete}/>
        <Resource name="upstreams" list={UpList} edit={UpEdit} create={UpCreate} remove={UpDelete} />
        <Resource name="locations" list={LocationList} edit={LocationEdit} create={LocationCreate} remove={LocationDelete} />
        <Resource name="applications" list={ApplicationList} edit={ApplicationEdit} create={ApplicationCreate} remove={ApplicationDelete}/>
        <Resource name="ssls"list={SSLList} edit={SSLEdit} create={SSLCreate} remove={SSLDelete}/>}
        <Resource name="proxytypes" list={ProxyList} edit={ProxyEdit} create={ProxyCreate}/>

    </Admin>
);

export default App;
