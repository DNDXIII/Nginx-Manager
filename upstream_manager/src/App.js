import React from 'react';

import { jsonServerRestClient, Admin, Resource } from 'admin-on-rest';
import { UpList, UpEdit, UpCreate, UpDelete } from './Upstreams';
import { ServerList, ServerCreate, ServerEdit, ServerDelete } from './Servers';
import { VirtualServerCreate, VirtualServerDelete, VirtualServerEdit, VirtualServerList } from './VirtualServers';
import { ProxyList, ProxyEdit, ProxyCreate } from './ProxyTypes';
import { SSLCreate, SSLDelete, SSLEdit, SSLList } from './SSLs';
import { ApplicationCreate, ApplicationDelete, ApplicationEdit, ApplicationList } from './Applications';
import { LocationCreate, LocationDelete, LocationEdit, LocationList } from './Locations';
import { GeneralConfigCreate, GeneralConfigList, GeneralConfigEdit, GeneralConfigDelete } from './GeneralConfig';
import { DeploymentServerList, DeploymentServerCreate, DeploymentServerEdit, DeploymentServerDelete } from './DeploymentServers';
import customRoutes from './customRoutes';
import Menu from './Menu';
import authClient from './authClient';

export var apiUrl = {
    base: 'localhost:5000/api',
    nginxReload: function (id) {
        return 'http://' + this.base + '/deploymentservers/reload/' + id;
    },
    downloadConfig: function () {
        return 'http://' + this.base + '/config/download';
    },
    testConfig: function () {
        return 'http://' + this.base + '/config/test';
    },
    getConfig: function () {
        return 'http://' + this.base + '/config';
    },
    getWebSocket: function(){
        return 'ws://'+ this.base;
    }, 
    deployConfig: function(){
        return 'http://' + this.base + '/config/deploy';
    },
    nginxShutdown: function (id) {
        return 'http://' + this.base + '/deploymentservers/shutdown/' + id;
    },
    nginxStart: function (id) {
        return 'http://' + this.base + '/deploymentservers/start/' + id;
    }
};

const App = () => (
    <Admin menu={Menu} customRoutes={customRoutes} /*authClient={authClient}*/ restClient={jsonServerRestClient("http://"+apiUrl.base)}>
        <Resource name="virtualservers" list={VirtualServerList} edit={VirtualServerEdit} create={VirtualServerCreate} remove={VirtualServerDelete} />
        <Resource name="servers" list={ServerList} edit={ServerEdit} create={ServerCreate} remove={ServerDelete} />
        <Resource name="upstreams" list={UpList} edit={UpEdit} create={UpCreate} remove={UpDelete} />
        <Resource name="locations" list={LocationList} edit={LocationEdit} create={LocationCreate} remove={LocationDelete} />
        <Resource name="applications" list={ApplicationList} edit={ApplicationEdit} create={ApplicationCreate} remove={ApplicationDelete} />
        <Resource name="deploymentservers" list={DeploymentServerList} edit={DeploymentServerEdit} create={DeploymentServerCreate} remove={DeploymentServerDelete} />
        <Resource name="ssls" list={SSLList} edit={SSLEdit} create={SSLCreate} remove={SSLDelete} />}
        <Resource name="proxytypes" list={ProxyList} edit={ProxyEdit} create={ProxyCreate} />
        <Resource name="generalconfig" list={GeneralConfigList} edit={GeneralConfigEdit} create={GeneralConfigCreate} delete={GeneralConfigDelete} />
    </Admin>
);

export default App;
