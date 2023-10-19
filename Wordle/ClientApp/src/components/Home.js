import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';

export class Home extends Component {
  static displayName = Home.name;

  state = {
    isAuthenticated: false,
    userName: null
  };

  async populateState() {
    const [isAuthenticated] = await Promise([authService.isAuthenticated()])
    this.setState({
      isAuthenticated,
    });
  }

  render() {
    const { isAuthenticated } = this.state;
    return (

      isAuthenticated ? "yes" :

      <div className='home-container'>
            <h1>Welcome to Wordle</h1>
            <button className='button-blue'> Nytt spel! </button>
      </div>
    );
  }
}
