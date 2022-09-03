import React from 'react';
import Aux from '../../hoc/Aux1/Aux1';
import Tags from '../../components/Tags/Tags';
import VideoShowcase from '../../components/VideoShowcase/VideoShowcase';
// import classes from './Home.module.css';

class Home extends React.Component {
  render() {
    return (
      <Aux>
        <Tags />
        <VideoShowcase />
      </Aux>
    );
  }
}

export default Home;
