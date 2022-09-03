import React from 'react';
import classes from './Center.module.css';
import Search from './search.png';

const Center = (props) => (
  <div className={classes.CenterWrapper}>
    <div className={classes.Center}>
      <input className={classes.SearchInput} placeholder="Search"></input>
      <button className={classes.SearchButton}>
        <img src={Search} alt="search"></img>
      </button>
    </div>
  </div>
);

export default Center;
