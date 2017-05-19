import React from 'react';

import { jsonServerRestClient, Admin, Resource } from 'admin-on-rest';
import { UpList, UpEdit, UpCreate, UpDelete } from './Upstreams';
import {ServerList, ServerCreate, ServerEdit, ServerDelete} from './Servers';
import {VirtualServerCreate, VirtualServerDelete, VirtualServerEdit, VirtualServerList} from './VirtualServers';
import {ProxyList,ProxyEdit, ProxyCreate} from './ProxyTypes';
import {SSLCreate, SSLDelete, SSLEdit, SSLList} from './SSLs';
import {ApplicationCreate, ApplicationDelete, ApplicationEdit, ApplicationList} from './Applications';
import {LocationCreate, LocationDelete, LocationEdit, LocationList} from './Locations';
import {GeneralConfigCreate, GeneralConfigList, GeneralConfigEdit, GeneralConfigDelete} from './GeneralConfig';


import Menu from './Menu';

import authClient from './authClient';

const App=()=> (
    <Admin menu={Menu} authClient={authClient} restClient={jsonServerRestClient('http://localhost:5000/api')}>
        <Resource name="virtualservers" list={VirtualServerList} edit={VirtualServerEdit} create={VirtualServerCreate} remove={VirtualServerDelete}/>
        <Resource name="servers" list={ServerList} edit={ServerEdit} create={ServerCreate} remove={ServerDelete}/>
        <Resource name="upstreams" list={UpList} edit={UpEdit} create={UpCreate} remove={UpDelete} />
        <Resource name="locations" list={LocationList} edit={LocationEdit} create={LocationCreate} remove={LocationDelete} />
        <Resource name="applications" list={ApplicationList} edit={ApplicationEdit} create={ApplicationCreate} remove={ApplicationDelete}/>
        <Resource name="ssls"list={SSLList} edit={SSLEdit} create={SSLCreate} remove={SSLDelete}/>}
        <Resource name="proxytypes" list={ProxyList} edit={ProxyEdit} create={ProxyCreate}/>
        <Resource name="generalconfig" list={GeneralConfigList} edit={GeneralConfigEdit} create={GeneralConfigCreate} delete={GeneralConfigDelete}/>

    </Admin>
);

export default App;
