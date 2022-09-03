import React from 'react';

import classes from './Start.module.css';
import Hamburger from './Hamburger.png';
import Logo from './Logo.png';

// import NavigationItems from '../NavigationItems/NavigationItems';
// import DrawerToggle from '../SideDrawer/DrawerToggle/DrawerToggle'

const start = (props) => (
  <div className={classes.Start}>
    <a href="/#" className={classes.Hamburger} onClick={props.hamburgerClicked}>
      <img src={Hamburger} alt="Hamburger Menu"></img>
    </a>
    <div className={classes.Logo}>
      <img src={Logo} alt="Youtube Brand Logo"></img>
    </div>
  </div>
);

export default start;
