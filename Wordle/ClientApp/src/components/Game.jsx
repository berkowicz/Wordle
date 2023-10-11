import React, { useEffect, useState } from 'react'
import axios from 'axios'
import Auth from './api-authorization/AuthorizeService'
let config;
let myToken;

/*
*  To do:
*   
*   Try to find game that is not completed
*   If not,
*   Start new game and populate it
* 
* */
const Game = () => {

 
    const [attempts, setAttempts] = useState([]);

    useEffect(() => {
        const fetchDataWithToken = async () => {
            myToken = await Auth.getAccessToken()
            config = {
                headers: myToken ? { 'Authorization': `Bearer ${myToken}` } : {}
            };
            FetchData();
        };

        fetchDataWithToken();
    }, []);


    const newGame = async () => {

        axios.post('/api/game', {}, config)
            .then(response => console.log(response))
            .catch(error => console.error(error));
        
        FetchData();
    }
    const FetchData = async () => {
        //Fetch a game 
        const response = await fetch('api/game', config)
            .then(data => {
                if(data.status === 404){
                    //No game
                    newGame()
                }
                else if(data.status === 200){
                    return data.json()
                }
                else{
                    throw new Error("Unable find or create game")
                }
            })
            .then(data => {
    
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
    </>
  )
}

export default Game