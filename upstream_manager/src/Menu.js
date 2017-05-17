import React from 'react';
import MenuItem from 'material-ui/MenuItem';
import { Link } from 'react-router-dom';

export default ({ resources, onMenuTap, logout }) => (
    <div>
        <MenuItem containerElement={<Link to="/virtualservers" />} primaryText="Virtual Servers" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/servers" />} primaryText="Servers" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/upstreams" />} primaryText="Upstreams" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/locations" />} primaryText="Locations" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/applications" />} primaryText="Applications" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/ssls" />} primaryText="SSL's" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/proxytypes" />} primaryText="Proxy Types" onTouchTap={onMenuTap} />
        <MenuItem primaryText="Generate Configuration" onTouchTap={handleClick} />
        {logout}
    </div>
);

function handleClick(){
    var url= "http://localhost:58370/api/generateconfig";
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open( "GET", url, true ); 
    xmlHttp.responseType='blob'
    xmlHttp.send( null );

    xmlHttp.onload = function(e) {
        if (this.status == 200) {
            // Create a new Blob object using the 
            //response data of the onload object
            var blob = new Blob([this.response], {type: 'text/plain'});
            //Create a link element, hide it, direct 
            //it towards the blob, and then 'click' it programatically
            let a = document.createElement("a");
            a.style = "display: none";
            document.body.appendChild(a);
            //Create a DOMString representing the blob 
            //and point the link element towards it
            let url = window.URL.createObjectURL(blob);
            a.href = url;
            a.download = 'configuration.config';
            //programatically click the link to trigger the download
            a.click();
            //release the reference to the file by revoking the Object URL
            window.URL.revokeObjectURL(url);
        }else{
            alert("something went wrong")
        }
    };
}