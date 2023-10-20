import React, { useEffect, useState } from "react";
import axios from "axios";
import Auth from "./api-authorization/AuthorizeService";
import Guess from "./Guess";
import Input from "./Input";
import Keyboard from "./Keyboard";
import GameOver from "./GameOver";

let config = { headers: {} }; //Request header to be filled with JWT token
let myToken;
let done = false;

const apiHost = "/api/game";

const Game = ({ resethandler }) => {
  const [attempts, setAttempts] = useState([]);
  const [guess, setGuess] = useState("");
  const [creatingNewGame, setCreatingNewGame] = useState(false);
  const [gameFinished, setGameFinished] = useState(false);
  const [gameOver, setGameOver] = useState(false);
  const [correctWord, setCorrectWord] = useState("");

  //Set token and request header config at load
  useEffect(() => {
    //Async function to fetch token
    const FetchDataWithToken = async () => {
      myToken = await Auth.getAccessToken(); //Get token
      config.headers = myToken ? { Authorization: `Bearer ${myToken}` } : {}; // Set request header

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

    axios
      .post(apiHost, {}, config)
      .then(() => FetchGame()) //Load recently created game
      .finally(() => setCreatingNewGame(false)); // Reset creatingNewGame state
  };

  //Fetch active game
  const FetchGame = async () => {
    done = false;

    const response = await fetch(apiHost, config)
      .then((data) => {
        //If no active game exist, crete a new game
        if (data.status === 404) {
          newGame();
        }
        //If request respond with a game, return as json
        else if (data.status === 200) {
          return data.json();
        }
        // Unknown error handling
        else {
          throw new Error("Unable find or create game");
        }
      })
      .then((data) => {
        //For each "Attempt"-key, assign it to attempts usestate
        for (let i = 1; i <= 5; i++) {
          const attemptKey = `attempt${i}`;
          const attemptValue = data[attemptKey];
          console.log(attemptValue);
          if (attemptValue != null) {
            setAttempts((prevAttempts) => [...prevAttempts, attemptValue]);
          }
        }
      });
  };

  //Send a guess after enter
  const SendGuess = async () => {
    //Add put method to header config
    let putConfig = {
      ...config,
      method: "PUT",
    };

    const response = await fetch(`${apiHost}/${guess}`, putConfig)
      .then((response) => response.json())
      .then((result) => {

        //Work around to make the json in same PascalCasing
        const resultWithUppercaseKeys = Object.keys(result).reduce(
          (acc, key) => {
            acc[key.charAt(0).toUpperCase() + key.slice(1)] = result[key];
            return acc;
          },
          {}
        );

        if (result.correct) {
          setGameFinished(true);
        }

        setAttempts((prevAttempts) => [
          ...prevAttempts,
          JSON.stringify(resultWithUppercaseKeys),
        ]);
        setCorrectWord(result.word);
      });

    setGuess("");
  };

  //Check if game is over
  useEffect(() => {
    if (!gameFinished && attempts.length === 5) {
      setGameOver(true);
    }
  }, [attempts]);

  //Check if game is over or finished. To make enter work for generating new game
  useEffect(() => {
    if (gameFinished || gameOver) {
      done = true;
    }
  }, [gameFinished, gameOver]);

  //Setting up keypress listener
  useEffect(() => {
    const handleKeyPress = async (event) => {

      var pressedKey = event.key;
      var pressedKeyCode = event.keyCode;
      var EnterKeyCode = 13;
      var DeleteKeyCode = 8;

      if (keyIsAllowed(pressedKeyCode)) {
        switch (pressedKeyCode) {
          case DeleteKeyCode:
            setGuess((guessValue) => guessValue.slice(0, -1));

            break;
          case EnterKeyCode:
            if (done) {
              handleReset();
            }

            if (guess.length === 5) {
              SendGuess();
            }

            break;
          default:
            if (guess.length < 5) {
              setGuess((guessValue) => guessValue + pressedKey);
            }

            break;
        }
      }
    };

    document.body.addEventListener("keydown", handleKeyPress);

    return () => {
      document.body.removeEventListener("keydown", handleKeyPress);
    };
  }, [guess]);

  //Function to return true if key is allowed
  const keyIsAllowed = (key) => {
    const AkeyCode = 65;
    const ZkeyCode = 90;
    var EnterKeyCode = 13;
    var DeleteKeyCode = 8;

    //A-Z, Å, Ä, Ö
    if (
      (key >= AkeyCode && key <= ZkeyCode) || //A-Z
      key === 222 || //Ä
      key === 221 || //Å
      key === 192 || //Ö
      key === DeleteKeyCode ||
      key === EnterKeyCode
    ) {
      return true;
    } else {
      return false;
    }
  };

  const handleReset = () => {
    resethandler();
  };

  return (
    <>
      <div className="game-container">
        {attempts.map((attempt) => (
          <>
            <Guess value={attempt} />
          </>
        ))}
        {gameFinished != true && !gameOver && (
          <div className=" guessword input active">
            <Input value={guess} />
          </div>
        )}
        {attempts.length < 4 &&
          Array(4 - attempts.length)
            .fill(null)
            .map((_, index) => (
              <>
                <div key={index} className=" guessword input ">
                  <Input />
                </div>
              </>
            ))}

        {gameFinished ? (
          <GameOver reset={handleReset} />
        ) : gameOver ? (
          <GameOver correct={correctWord} reset={handleReset} />
        ) : (
          ""
        )}
      </div>

      <Keyboard value={attempts} />
    </>
  );
};

export default Game;
