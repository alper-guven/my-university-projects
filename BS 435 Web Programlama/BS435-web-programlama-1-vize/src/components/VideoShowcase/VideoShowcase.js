import React from 'react';
import YTVideo from './Video/Video';

import classes from './VideoShowcase.module.css';

const videoShowcase = (props) => {
  const channelNames = [
    'Best Channel Ever',
    'Tech Tech',
    'To The Moon',
    'Space',
    'Brand New Cars',
    'Mars',
  ];

  const videoNames = ['Lorem', 'Ipsum', 'Dolor', 'Sit', 'Amet'];

  const videos = [];

  for (let i = 0; i < 30; i++) {
    let title = [
      videoNames[i % 5],
      videoNames[i % 2],
      videoNames[i % 7],
      (i % 7) - 5,
    ].join(' ');
    let channel = channelNames[i % 6];
    let views = (i % 3) * 10;
    let daysAgo = (i % 4) + 2;
    videos.push(
      <YTVideo
        title={title}
        channel={channel}
        views={views}
        daysAgo={daysAgo}
        key={i}
      ></YTVideo>
    );
  }

  return <div className={classes.VideoShowcase}>{videos}</div>;
};

export default videoShowcase;
