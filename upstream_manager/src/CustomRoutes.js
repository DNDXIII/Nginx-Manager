import React from 'react';
import { Route } from 'react-router';
import {GenerateConfig} from './GenerateConfig';

export default () =>(
    <Route exact path="/generateconfig" component={GenerateConfig} />
);