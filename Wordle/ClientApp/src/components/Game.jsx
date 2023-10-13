import React, { useEffect, useState } from 'react'
import axios from 'axios'
import Auth from './api-authorization/AuthorizeService'

let config = { headers: {} }; //Request header to be filled with JWT token
let myToken;

const apiHost = '/api/game';

/*
*  To do:
*   
*  Input fielad for guesses
*  On submit = send request to api/game{gameid}/{guess}
*  Listen to response
* 
* */
const Game = () => {

 
    const [attempts, setAttempts] = useState([]);


    //Set token and request header config at load
    useEffect(() => {

        //Async function to fetch token
        const FetchDataWithToken = async () => {
            myToken = await Auth.getAccessToken()
            config = {
                headers: myToken ? { 'Authorization': `Bearer ${myToken}` } : {}
            };
            FetchGame();
        };

        FetchDataWithToken();
    }, []);


    //Create a new game in the database
    const newGame = async () => {

        axios.post(apiHost, {}, config)

        //Load recently created game
        FetchGame();
    }



    //Fetch active game
    const FetchGame = async () => {
        
        const response = await fetch(apiHost, config)
            .then(data => {

                //If no active game exist, crete a new game
                if(data.status === 404){
                    newGame()
                }
                //If request respond with a game, return as json
                else if(data.status === 200){
                    return data.json()
                }
                // Unknown error handling
                else{
                    throw new Error("Unable find or create game")
                }
            })
            .then(data => {

                //For each "Attempt"-key, assign it to attempts usestate
                for (let i = 1; i <= 5; i++) {
                    const attemptKey = `attempt${i}`;
                    const attemptValue = data[attemptKey];
                    setAttempts((prevAttempts) => [...prevAttempts, attemptValue]);
                }

            })
    }      


  return (
    <>
    {}
    <div>Game</div>

    {
      attempts.map(prop => (
        prop ? prop : <div>Empty</div>

      ))
          }

          <div>Input fields</div>
          
          
    </>
  )
}

export default Game