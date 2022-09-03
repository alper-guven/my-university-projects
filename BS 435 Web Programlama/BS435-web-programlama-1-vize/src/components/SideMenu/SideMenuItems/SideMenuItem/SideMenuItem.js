import React from 'react';

import classes from './SideMenuItem.module.css';


const sideMenuItem = (props) => (
  <div className={classes.SideMenuItem}>
    <a href="/#" className={classes.Hamburger} onClick={props.hamburgerClicked}>
      <img src={props.icon} alt="Side Menu Icon"></img>
      <div className={classes.Logo}>{props.value}</div>
    </a>
  </div>
);

export default sideMenuItem;
