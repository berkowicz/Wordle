import React, { useEffect, useState } from 'react'
import axios from 'axios'
import Auth from './api-authorization/AuthorizeService'


async function someFunction() {
  try {
    // Call the getAccessToken method directly on the Auth class
    const accessToken = await Auth.getAccessToken();
    const user = await Auth.getUser();

    // Now you can use the accessToken as needed
    console.log("Auth")
    console.log(accessToken);
    console.log(user);

  } catch (error) {
    console.error('Error:', error);
  }
}

const Game = () => {

  const [userId, setUserId] = useState("");
  const [attempts, setAttempts] = useState([]);


  useEffect(() => {
    UserId()
  
  }, [])

  useEffect(() => {
    axios
    .get('https://localhost:44479/api/Game/' + userId)
    .then(data => {
      console.log("user lodaded")
      console.log(data.data)

      for (let i = 1; i <= 5; i++) {
        const attemptKey = `attempt${i}`;
        const attemptValue = data.data[attemptKey];
        
        setAttempts((prevAttempts) => [...prevAttempts, attemptValue]);
      }
      console.log(attempts)

    })

    console.log("Heh")
    someFunction()

  
  }, [userId])


  


  const UserId = () => {
    const token = localStorage.getItem('Wordleuser:https://localhost:44479:Wordle')
    const parsedData = JSON.parse(token)
    const profile = parsedData.profile.sub
    console.log(profile);
    setUserId(profile);
  
  }

  return (
    <>
    {}
    <div>Game</div>
    {
      userId && <p>{userId}</p>
    }
    {
      attempts.map(prop => (
        prop ? prop : <div>Empty</div>

      ))
    }
    </>
  )
}

export default Game