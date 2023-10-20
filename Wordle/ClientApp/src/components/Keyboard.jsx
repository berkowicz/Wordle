import React, { useEffect, useState } from "react";

const Keyboard = ({ value }) => {
  const [attempts, setAttempts] = useState([]);
  const [arr, setArr] = useState([]);
  const [keys, setKeys] = useState([]);

  useEffect(() => {
    setAttempts(value);
  }, [value]);

  useEffect(() => {
    setArr(attempts.map(JSON.parse));
  }, [attempts]);

  useEffect(() => {
    setKeys(["qwertyuiopå", "asdfghjklöä", "zxcvbnm"]);
  }, [arr]);

  //Check attempt array, from bottom to top, if it contain letter and letterstatus.
  const findLetterStatus = (attemptsArray, letterToFind) => {
    for (let i = attemptsArray.length - 1; i >= 0; i--) {
      const guess = attemptsArray[i].Guess;
      const letterStatus = attemptsArray[i].LetterStatus;

      if (guess.includes(letterToFind)) {
        const index = guess.indexOf(letterToFind);
        if (letterStatus[index] === "1") {
          return "1";
        } else if (letterStatus[index] === "2") {
          return "2";
        } else if (letterStatus[index] === "3") {
          return "3";
        }
      }
    }
    return null;
  };

  return (
    keys && (
      <div className="keyboard">
        {keys.map((row, rowIndex) => (
          <div key={rowIndex}>
            {/* For each key in the keyboard, check letterstatus */}
            {row.split("").map((letter, letterIndex) =>
              (
                <div
                className={`letter l-small letter-${findLetterStatus(arr, letter) || ""}`}
                key={letterIndex}
              >
                {letter}
              </div>
              )
            )}
          </div>
        ))}
      </div>
    )
  );
};

export default Keyboard;
