import React from 'react';

import classes from './Video.module.css';

import Thumbnail from './thumbnail.jpg';
import Profile from './profile.png';

const ytVideo = (props) => (
  <div className={classes.Video}>
    <a href="/#">
      <img src={Thumbnail} alt="Video Thumbnail"></img>
      <div className={classes.Info}>
        <div className={classes.LeftCol}>
          <img src={Profile} alt="Profile"></img>
        </div>
        <div className={classes.Details}>
          <span className={classes.Title}>{props.title}</span>
          <span>{props.channel}</span>
          <span>{props.views}K views | {props.daysAgo} days ago</span>
        </div>
      </div>
    </a>
  </div>
);

export default ytVideo;
