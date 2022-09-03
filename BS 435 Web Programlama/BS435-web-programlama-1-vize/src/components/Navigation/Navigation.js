import React from 'react';

import classes from './Navigation.module.css';
// import NavigationItems from '../NavigationItems/NavigationItems';
// import DrawerToggle from '../SideDrawer/DrawerToggle/DrawerToggle'
import Start from './Start/Start';
import Center from './Center/Center';
import End from './End/End';

const start = (props) => (
  <header className={classes.Navigation}>
    <Start hamburgerClicked={props.menuToggleClicked}/>
    <Center />
    <End />
  </header>
);

export default start;
