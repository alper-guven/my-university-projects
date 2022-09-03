import React, { Component } from 'react';

import Aux from '../Aux1/Aux1';
import classes from './Layout.module.css';
import Navigation from '../../components/Navigation/Navigation';
import SideMenu from '../../components/SideMenu/SideMenu';

class Layout extends Component {
  state = {
    showSideMenu: false,
  };

  sideMenuToggleHandler = () => {
    this.setState((prevState) => {
      return { showSideMenu: !prevState.showSideMenu };
    });
  };

  render() {
    let attachedClasses = [classes.Content];

    if (this.state.showSideMenu) {
      attachedClasses = [classes.Content, classes.SideMenuOpen];
    }

    return (
      <Aux>
        <Navigation menuToggleClicked={this.sideMenuToggleHandler} />
        <SideMenu open={this.state.showSideMenu} />
        <main className={attachedClasses.join(' ')}>{this.props.children}</main>
      </Aux>
    );
  }
}

export default Layout;
