import React from 'react';
import MenuItem from 'material-ui/MenuItem';
import { Link } from 'react-router';

export default ({ resources, onMenuTap, logout }) => (
    <div>
        <MenuItem containerElement={<Link to="/virtualservers" />} primaryText="Virtual Servers" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/servers" />} primaryText="Servers" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/upstreams" />} primaryText="Upstreams" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/locations" />} primaryText="Locations" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/applications" />} primaryText="Applications" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/ssls" />} primaryText="SSL's" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/proxytypes" />} primaryText="Proxy Types" onTouchTap={onMenuTap} />
        <MenuItem containerElement={<Link to="/generateconfig" />} primaryText="Generate Configuration" onTouchTap={onMenuTap} />
        {logout}
    </div>
);