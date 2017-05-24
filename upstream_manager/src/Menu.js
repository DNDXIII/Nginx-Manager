import React from 'react';
import MenuItem from 'material-ui/MenuItem';
import { Link } from 'react-router-dom';
import Download from 'material-ui/svg-icons/file/file-download';
import Router from 'material-ui/svg-icons/hardware/router';
import Tethering from 'material-ui/svg-icons/device/wifi-tethering';
import Settings from 'material-ui/svg-icons/action/settings';
import Wifi from 'material-ui/svg-icons/device/signal-wifi-4-bar';
import DeviceHub from 'material-ui/svg-icons/hardware/device-hub';
import Place from 'material-ui/svg-icons/maps/place';
import LineWeight from 'material-ui/svg-icons/action/line-weight';
import Lock from 'material-ui/svg-icons/action/lock';
import Build from 'material-ui/svg-icons/action/build';



export default ({ resources, onMenuTap, logout }) => (
    <div>
        <MenuItem containerElement={<Link to="/generalconfig" />} primaryText="General Config" onTouchTap={onMenuTap} leftIcon={<Settings />} />
        <MenuItem containerElement={<Link to="/servers" />} primaryText="Servers" onTouchTap={onMenuTap} leftIcon={<Wifi />} />
        <MenuItem containerElement={<Link to="/virtualservers" />} primaryText="Virtual Servers" onTouchTap={onMenuTap} leftIcon={<Tethering />} />
        <MenuItem containerElement={<Link to="/upstreams" />} primaryText="Upstreams" onTouchTap={onMenuTap} leftIcon={<DeviceHub />} />
        <MenuItem containerElement={<Link to="/locations" />} primaryText="Locations" onTouchTap={onMenuTap} leftIcon={<Place />} />
        <MenuItem containerElement={<Link to="/applications" />} primaryText="Applications" onTouchTap={onMenuTap} leftIcon={<LineWeight />} />
        <MenuItem containerElement={<Link to="/ssls" />} primaryText="SSL's" onTouchTap={onMenuTap} leftIcon={<Lock />} />
        <MenuItem containerElement={<Link to="/proxytypes" />} primaryText="Proxy Types" onTouchTap={onMenuTap} leftIcon={<Router />} />
        <MenuItem primaryText="Test Configuration" onTouchTap={handleTestConfig} leftIcon={<Build />} />
        <MenuItem primaryText="Generate Configuration" onTouchTap={handleDownload} leftIcon={<Download />} />
        {logout}
    </div>
);

function handleTestConfig() {
    var url = "http://localhost:5000/api/config/test";
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);

    xmlHttp.onload = function (e) {
        if (this.status == 200) 
            alert("The test was successfull:\n "+ this.responseText);
        else if (this.status==400)
            alert("The test failed:\n "+ this.responseText);
        else
            alert("Something went wrong.");
    }
}

function handleDownload() {
    var url = "http://localhost:5000/api/generateconfig";
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", url, true);
    xmlHttp.responseType = 'blob'
    xmlHttp.send(null);

    xmlHttp.onload = function (e) {
        if (this.status == 200) {
            // Create a new Blob object using the 
            //response data of the onload object
            var blob = new Blob([this.response], { type: 'text/plain' });
            //Create a link element, hide it, direct 
            //it towards the blob, and then 'click' it programatically
            let a = document.createElement("a");
            a.style = "display: none";
            document.body.appendChild(a);
            //Create a DOMString representing the blob 
            //and point the link element towards it
            let url = window.URL.createObjectURL(blob);
            a.href = url;
            a.download = 'configuration.conf';
            //programatically click the link to trigger the download
            a.click();
            //release the reference to the file by revoking the Object URL
            window.URL.revokeObjectURL(url);
        } else {
            alert("Something went wrong.")
        }
    };
}