import React from 'react';

import { jsonServerRestClient, Admin, fetchUtils, Resource } from 'admin-on-rest';
import { UpList, UpEdit, UpCreate, UpDelete } from './Upstreams';
import { ServerList, ServerCreate, ServerEdit, ServerDelete } from './Servers';
import { VirtualServerCreate, VirtualServerDelete, VirtualServerEdit, VirtualServerList } from './VirtualServers';
import { ProxyList, ProxyEdit, ProxyCreate } from './ProxyTypes';
import { SSLCreate, SSLDelete, SSLEdit, SSLList } from './SSLs';
import { ApplicationCreate, ApplicationDelete, ApplicationEdit, ApplicationList } from './Applications';
import { LocationCreate, LocationDelete, LocationEdit, LocationList } from './Locations';
import {  GeneralConfigList, GeneralConfigEdit } from './GeneralConfig';
import { DeploymentServerList, DeploymentServerCreate, DeploymentServerEdit, DeploymentServerDelete } from './DeploymentServers';
import { WhitelistList, WhitelistCreate, WhitelistEdit, WhitelistDelete } from './Whitelists';
import customRoutes from './customRoutes';
import Menu from './Menu';
import authClient from './authClient';

export const apiUrl = {

    //base: 'nginxmanager.northeurope.cloudapp.azure.com/api',
    base: 'localhost:5000/api',
    nginxReload: function (id) {
        return 'http://' + this.base + '/deploymentservers/reload/' + id;
    },  
    authenticate: function () {
        return 'http://' + this.base + '/jwt';
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
    getWebSocket: function () {
        return 'ws://' + this.base+'/ws';
    },
    deployConfig: function () {
        return 'http://' + this.base + '/config/deploy';
    },
    nginxShutdown: function (id) {
        return 'http://' + this.base + '/deploymentservers/shutdown/' + id;
    },
    nginxStart: function (id) {
        return 'http://' + this.base + '/deploymentservers/start/' + id;
    }
};


const httpClient = (url, options = {}) => {
    if (!options.headers) {
        options.headers = new Headers({ Accept: 'application/json' });
    }
    const token = localStorage.getItem('token');
    options.headers.set('Authorization', `Bearer ${token}`);
    return fetchUtils.fetchJson(url, options);
}
const restClient = jsonServerRestClient("http://" + apiUrl.base, httpClient);

const App = () => (
    <Admin menu={Menu} customRoutes={customRoutes} authClient={authClient} restClient={restClient}>
        <Resource name="virtualservers" list={VirtualServerList} edit={VirtualServerEdit} create={VirtualServerCreate} remove={VirtualServerDelete} />
        <Resource name="servers" list={ServerList} edit={ServerEdit} create={ServerCreate} remove={ServerDelete} />
        <Resource name="upstreams" list={UpList} edit={UpEdit} create={UpCreate} remove={UpDelete} />
        <Resource name="applications" list={ApplicationList} edit={ApplicationEdit} create={ApplicationCreate} remove={ApplicationDelete} />
        <Resource name="whitelists" list={WhitelistList} edit={WhitelistEdit} create={WhitelistCreate} remove={WhitelistDelete} />
        <Resource name="deploymentservers" list={DeploymentServerList} edit={DeploymentServerEdit} create={DeploymentServerCreate} remove={DeploymentServerDelete} />
        <Resource name="ssls" list={SSLList} edit={SSLEdit} create={SSLCreate} remove={SSLDelete} />}
        <Resource name="proxytypes" list={ProxyList} edit={ProxyEdit} create={ProxyCreate} />
        <Resource name="generalconfig" list={GeneralConfigList} edit={GeneralConfigEdit} />
    </Admin>
);

export default App;
