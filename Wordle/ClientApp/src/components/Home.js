import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div className='home-container'>
            <h1>Welcome to Wordle</h1>
      </div>
    );
  }
}
