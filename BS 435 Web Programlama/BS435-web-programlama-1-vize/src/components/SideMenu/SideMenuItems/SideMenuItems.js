import React from 'react';
import Aux from '../../../hoc/Aux1/Aux1';

import classes from './SideMenuItems.module.css';

import SideMenuItem from './SideMenuItem/SideMenuItem';

const sideMenuItems = (props) => {
  const items = [];

  for (const [, value] of props.elementList.entries()) {
    items.push(<SideMenuItem icon={props.icon} value={value}></SideMenuItem>);
  }

  return (
    <Aux>
      <div className={classes.SideMenuItemsWrapper}>
        {props.heading ? <h4>{props.heading}</h4> : null}
        <div className={classes.SideMenuItems}>{items}</div>
      </div>
    </Aux>
  );
};

export default sideMenuItems;
