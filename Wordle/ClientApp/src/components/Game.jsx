import React, { useEffect, useState } from 'react'
import axios from 'axios'
import Auth from './api-authorization/AuthorizeService'
import Guess from './Guess';

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
    const [guess, setGuess] = useState("");
    const [guessCount, setGuessCount] = useState(0);


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
                
                
                
                console.log("Orginal")
                console.log(data)
                

                //For each "Attempt"-key, assign it to attempts usestate
                for (let i = 1; i <= 5; i++) {
                    const attemptKey = `attempt${i}`;
                    const attemptValue = data[attemptKey]
                    console.log(attemptValue)
                    if(attemptValue != null){

                        setAttempts((prevAttempts) => [...prevAttempts, attemptValue]);

                    }
                }

            })
    }

    const SendGuess = async () => {

        //Add put method to header config
        let putConfig = {
            ...config,
            method: 'PUT'
          };

        const response = await fetch(`${apiHost}/${guess}`, putConfig)
            .then(data => data.json())
            .then(result => {
                const resultWithUppercaseKeys = Object.keys(result).reduce((acc, key) => {
                    acc[key.charAt(0).toUpperCase() + key.slice(1)] = result[key];
                    return acc;
                }, {});



                console.log("Skicka")
                console.log(resultWithUppercaseKeys)
                
                setAttempts((prevAttempts) => [...prevAttempts, JSON.stringify(resultWithUppercaseKeys)]);

            })
    }

    

    useEffect(() => {
        if (guess.length === 5) {
            console.log("5 letters!");
            SendGuess();
            setGuess("");
        }
    }, [guess])


    useEffect(() => {
        console.log(attempts)
    }, [attempts])


  return (
    <>
    {
      attempts.map(prop => (
        prop ? 
        <Guess value={ prop } />: 
        <div>Empty</div>

      ))
          }

          <div>Input fields</div>
          <form>
            <input type="text" name="input" id="guessinput" value={ guess } onChange={ (e) => setGuess(e.target.value) } />
          </form>
          
    </>
  )
}

export default Game