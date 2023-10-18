import React, { useEffect, useState } from 'react'
import axios from 'axios'
import Auth from './api-authorization/AuthorizeService'
import Guess from './Guess';
import Input from './Input';

let config = { headers: {} }; //Request header to be filled with JWT token
let myToken;

const apiHost = '/api/game';



const Game = () => {

    const [attempts, setAttempts] = useState([]);
    const [guess, setGuess] = useState('');
    const [creatingNewGame, setCreatingNewGame] = useState(false); 
    const [gameFinished, setGameFinished] = useState(false)
    const [gameOver, setGameOver] = useState(false)
    const [correctWord, setCorrectWord] = useState("")



    //Set token and request header config at load
    useEffect(() => {

        //Async function to fetch token
        const FetchDataWithToken = async () => {

            myToken = await Auth.getAccessToken(); //Get token
            config.headers = myToken ? { 'Authorization': `Bearer ${myToken}` } : {} // Set request header
            
            FetchGame(); // Try to fetch a game
        
        };



        FetchDataWithToken();
    }, []);

  


    //Create a new game in the database
    const newGame = async () => {

        //Check to prevent that multiple games are created
        if (creatingNewGame) {
            return;
        }


        setCreatingNewGame(true); //Set creatingNewGame state

        axios.post(apiHost, {}, config)
        .then(() => FetchGame()) //Load recently created game
        .finally(() => setCreatingNewGame(false)) // Reset creatingNewGame state

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

                if(result.correct){
                    setGameFinished(true);
                }
                

                setAttempts((prevAttempts) => [...prevAttempts, JSON.stringify(resultWithUppercaseKeys)]);
                setCorrectWord(result.word)
            })

        setGuess('');
    }

    useEffect(() => {
        if(!gameFinished && attempts.length === 5){
            setGameOver(true);
        }

    }, [attempts]);

    

 //Setting up keypress
 useEffect(() => {
        
    const handleKeyPress = async (event) => 
    {
        console.log("pressed")

        var pressedKey = event.key;
        var pressedKeyCode = event.keyCode;
        var EnterKeyCode = 13;
        var DeleteKeyCode = 8;
        var test = keyIsAllowed(pressedKeyCode)
        
        if (keyIsAllowed(pressedKeyCode)){

            switch( pressedKeyCode ) {

                case DeleteKeyCode:

                setGuess(guessValue => guessValue.slice(0, -1))
                    
                    break;
                case EnterKeyCode:
                    if(guess.length === 5)
                    {

                        SendGuess();
                    }
                    
                    break;
                default:
                    if(guess.length < 5){

                        setGuess(guessValue => guessValue + pressedKey)
                    }        
                        
                break;

                }

            }

     };




    document.body.addEventListener('keydown', handleKeyPress);

    return () => {
      document.body.removeEventListener('keydown', handleKeyPress);
    };
  }, [guess]);


    const keyIsAllowed = (key) => {
        const AkeyCode = 65;
        const ZkeyCode = 90;
        var EnterKeyCode = 13;
        var DeleteKeyCode = 8;

        //A-Z, Å, Ä, Ö
        if ((key >= AkeyCode && key <= ZkeyCode) || key === 222 || key === 221 || key === 192 || key === DeleteKeyCode || key === EnterKeyCode) {
            return true;
        }
        else {
            return false;
        }


    }




  return (
    <div className='game-container'>
    {
      attempts.map(attempt => (
         <>
        <Guess value={ attempt } />

         </>
      ))


          }
        {
        gameFinished != true && !gameOver &&  <div className=' guessword input active'>
        <Input value={ guess }  />

        </div>
        }
        {
        attempts.length < 4 && Array(4 - attempts.length).fill(null).map((_, index) => (
            <>

            <div key={ index } className=' guessword input '>
            <Input  />
            </div>


            </>
        ))
    }

        {gameFinished ?  <div className='finishedGame'>Du klarade det!
        </div> : gameOver ? <div className='finishedGame'>Game over! <br/> {correctWord} </div>  : ""}

    </div>
  )
}

export default Game