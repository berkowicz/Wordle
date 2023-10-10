import React, { useEffect, useState } from 'react'
import axios from 'axios'
import Auth from './api-authorization/AuthorizeService'

let config;

const newGame = async () => {
    const token = await Auth.getAccessToken();

    const config = {
        headers: token ? { 'Authorization': `Bearer ${token}` } : {}
    };

    axios.post('/api/game', {}, config)
        .then(response => console.log(response))
        .catch(error => console.error(error));
}


const Game = () => {

  //const [userId, setUserId] = useState("");
  const [attempts, setAttempts] = useState([]);


    const FetchData = async () => {
        const token = await Auth.getAccessToken();
        const response = await fetch('api/game', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }

        })
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

  useEffect(() => {
      //UserId()
      FetchData()

  
  }, [])

  //useEffect(() => {
  
  //}, [userId])

  

 


  //const UserId = () => {
  //  const token = localStorage.getItem('Wordleuser:https://localhost:44479:Wordle')
  //  const parsedData = JSON.parse(token)
  //  const profile = parsedData.profile.sub
  //  console.log(profile);
  //  setUserId(profile);
  
  //}

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