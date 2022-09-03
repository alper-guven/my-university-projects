import React from 'react';

import classes from './Tags.module.css';

const tags = (props) => {
  const tags = [
    'system',
    'networking',
    'electron',
    'based',
    'semiconductor',
    'innovative',
    'enterprise',
    'global',
    'hardware',
    'uses',
    'focusing',
    'enabled',
    'core',
    'telecommunications',
    'product',
    'data',
    'sophisticated',
    'market',
    'focused',
    'industrial',
    'design',
    'stateoftheart',
    'makers',
    'components',
    'use',
    'processing',
    'foraging',
    'improve',
    'using',
    'hominids',
  ];

  const items = [];

  for (const [index, value] of tags.entries()) {
    items.push(<a href="/#" key={index}>{value}</a>);
  }

  return <div className={classes.Tags}>{items}</div>;
};

export default tags;
