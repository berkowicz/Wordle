import React from 'react';
import { Link, useLocation, NavLink } from 'react-router-dom';

const Error = () => {
  const location = useLocation();

  return (
    <div>
      <h2>Sidan finns inte</h2>
      <p>Du har försökt nå: {location.pathname}</p>
      <NavLink tag={Link} className="newgame" to="/">
        Återgå till spelsidan
      </NavLink>
    </div>
  );
};

export default Error;
