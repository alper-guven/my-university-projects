import React from 'react';
import classes from './End.module.css';

import Bell from './bell.png';
import Record from './record.png';
import SquareMenu from './square-menu.png';
import Profile from './profile.png';
import { NavLink } from 'react-router-dom';

const end = (props) => (
  <div className={classes.End}>
    <a href="/#" className={classes.EndLink}>
      <img src={Record} alt="Upload Video"></img>
    </a>
    <a href="/#" className={classes.EndLink}>
      <img src={SquareMenu} alt="Menu"></img>
    </a>
    <a href="/#" className={classes.EndLink}>
      <img src={Bell} alt="Notifications"></img>
    </a>
    <NavLink to="/profile" className={classes.EndLink} exact={true}>
      <img src={Profile} alt="Profile"></img>
    </NavLink>
  </div>
);

export default end;
