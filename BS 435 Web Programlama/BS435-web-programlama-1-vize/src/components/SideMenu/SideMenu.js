import React from 'react';
import Aux from '../../hoc/Aux1/Aux1';

import classes from './SideMenu.module.css';
import SideMenuItems from './SideMenuItems/SideMenuItems';

import HomeIcon from './SideMenuItems/SideMenuItem/Home.png';
import ProfileIcon from './SideMenuItems/SideMenuItem/profile.png';

const sideMenu = (props) => {
  let attachedClasses = [classes.SideMenu, classes.Close];

  if (props.open) {
    attachedClasses = [classes.SideMenu, classes.Open];
  }

  const topSectionElements = ['Home', 'Trending', 'Subscriptions'];
  const secondSectionElements = [
    'Library',
    'History',
    'Your Videos',
    'Watch Later',
    'Liked videos',
    'Show More',
  ];

  const subsSectionElements = [
    'User 1',
    'User 2',
    'User 3',
    'User 4',
    'User 5',
    'User 6',
  ];

  const moreSectionElements = ['Youtube Premium', 'Gaming', 'Live'];
  const lastSectionElements = [
    'Settings',
    'Report History',
    'Help',
    'Send feedback',
  ];

  return props.open ? (
    <Aux>
      <div className={attachedClasses.join(' ')}>
        <SideMenuItems elementList={topSectionElements} icon={HomeIcon} />
        <SideMenuItems elementList={secondSectionElements} icon={HomeIcon} />
        <SideMenuItems
          elementList={subsSectionElements}
          heading="SUBSCRIPTIONS"
          icon={ProfileIcon}
        />
        <SideMenuItems
          elementList={moreSectionElements}
          heading="MORE FROM YOUTUBE"
          icon={HomeIcon}
        />
        <SideMenuItems elementList={lastSectionElements} icon={HomeIcon} />
      </div>
    </Aux>
  ) : null;
};

export default sideMenu;
