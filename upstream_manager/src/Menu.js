import React from 'react';
import MenuItem from 'material-ui/MenuItem';
import Divider from 'material-ui/Divider';
import { Link } from 'react-router-dom';
import Router from 'material-ui/svg-icons/hardware/router';
import Tethering from 'material-ui/svg-icons/device/wifi-tethering';
import Settings from 'material-ui/svg-icons/action/settings';
import Wifi from 'material-ui/svg-icons/device/signal-wifi-4-bar';
import DeviceHub from 'material-ui/svg-icons/hardware/device-hub';
import Place from 'material-ui/svg-icons/maps/place';
import LineWeight from 'material-ui/svg-icons/action/line-weight';
import Lock from 'material-ui/svg-icons/action/lock';
import Prohibited from 'material-ui/svg-icons/av/not-interested';





export default ({ resources, onMenuTap, logout }) => (
    <div>
        <MenuItem containerElement={<Link to="/generalconfig" />} primaryText="General Config" onTouchTap={onMenuTap} leftIcon={<Settings />} />
        <Divider />
        <MenuItem containerElement={<Link to="/servers" />} primaryText="Servers" onTouchTap={onMenuTap} leftIcon={<Wifi />} />
        <MenuItem containerElement={<Link to="/upstreams" />} primaryText="Upstreams" onTouchTap={onMenuTap} leftIcon={<DeviceHub />} />
        <Divider />
        <MenuItem containerElement={<Link to="/locations" />} primaryText="Locations" onTouchTap={onMenuTap} leftIcon={<Place />} />
        <MenuItem containerElement={<Link to="/applications" />} primaryText="Applications" onTouchTap={onMenuTap} leftIcon={<LineWeight />} />

        <MenuItem containerElement={<Link to="/virtualservers" />} primaryText="Virtual Servers" onTouchTap={onMenuTap} leftIcon={<Tethering />} />
        <Divider />
        <MenuItem containerElement={<Link to="/blacklists" />} primaryText="Whitelist" onTouchTap={onMenuTap} leftIcon={<Prohibited />} />
        <MenuItem containerElement={<Link to="/ssls" />} primaryText="SSL's" onTouchTap={onMenuTap} leftIcon={<Lock />} />
        <MenuItem containerElement={<Link to="/proxytypes" />} primaryText="Proxy Types" onTouchTap={onMenuTap} leftIcon={<Router/*change*/ />} />
        <Divider />
        <MenuItem containerElement={<Link to="/deploymentservers" />} primaryText="Deployment Servers" onTouchTap={onMenuTap} leftIcon={<Router />} />
        <MenuItem containerElement={<Link to="/config" />} primaryText="Deploy Configuration" onTouchTap={onMenuTap} leftIcon={<Place />} />
        <Divider />
        {logout}
    </div>
);

