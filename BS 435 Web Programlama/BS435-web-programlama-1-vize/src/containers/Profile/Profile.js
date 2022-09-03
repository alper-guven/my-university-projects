import React from 'react';
import Aux from '../../hoc/Aux1/Aux1';
import classes from './Profile.module.css';

class Profile extends React.Component {
  render() {
    return (
      <Aux>
        <div className={classes.ProfileWrapper}>
          <h1>Alper Güven</h1>
          <p>
            I deliver full stack web applications with SPA on front-end which
            created using a modern front-end framework such as Angular or React,
            backed by a NodeJS application.
          </p>
          <p>
            I also sell single/multi page promotional websites for SMEs (small
            and medium-sized enterprises) with a seamless turnkey experience.
          </p>
          <a href="https://www.linkedin.com/in/alperguven/">
            Check My LinkedIn Profile!
          </a>
        </div>
      </Aux>
    );
  }
}

export default Profile;
