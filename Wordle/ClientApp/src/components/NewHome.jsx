import React, { useEffect, useState } from 'react'
import Auth from './api-authorization/AuthorizeService'
import Game from './Game';


const NewHome = () => {

    const [isAuthenticated, setIsAuthenticated] = useState(false)

    useEffect(() => {

        //Async function to fetch token
        const FetchDataWithToken = async () => {

            const authStatus = await Auth.isAuthenticated();
            setIsAuthenticated(authStatus)
        
        };



        FetchDataWithToken();
    }, []);

    useEffect(() => {
        console.log("first")
        console.log(isAuthenticated)
    }, [isAuthenticated]);
  return (

    isAuthenticated ? <Game /> :
    <div>Logga in!</div> 
  )
}

export default NewHome