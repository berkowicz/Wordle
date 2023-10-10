import React, { useEffect, useState } from 'react'
import axios from 'axios'
import Auth from './api-authorization/AuthorizeService'



const Game = () => {

    let config;
    let myToken;
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
    }
    const FetchData = async () => {
        //Fetch a game 
        const response = await fetch('api/game', config)
            .then(data => data.json())
            .then(data => {
                console.log("user lodaded")
                console.log(data)

                for (let i = 1; i <= 5; i++) {
                    const attemptKey = `attempt${i}`;
                    const attemptValue = data[attemptKey];
                    console.log(attemptKey)
                    setAttempts((prevAttempts) => [...prevAttempts, attemptValue]);
                }
                console.log(attempts)

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

          <button onClick={ newGame }>Nytt spel</button>
    </>
  )
}

export default Game