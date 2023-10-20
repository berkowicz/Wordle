import React, { useEffect, useState } from "react";
import Auth from "./api-authorization/AuthorizeService";
import Game from "./Game";

const NewHome = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [seed, setSeed] = useState(1);

  useEffect(() => {
    //Async function to fetch token
    const FetchDataWithToken = async () => {
      const authStatus = await Auth.isAuthenticated();
      setIsAuthenticated(authStatus);
    };

    FetchDataWithToken();
  }, []);

  useEffect(() => {
  }, [isAuthenticated]);

  //Workaround to set random number as key to reload game component 
  const changeSeed = () => {
    setSeed(Math.random());
  };

  return isAuthenticated ? (
    <Game key={seed} resethandler={changeSeed} />
  ) : (
    <div>Logga in!</div>
  );
};

export default NewHome;
