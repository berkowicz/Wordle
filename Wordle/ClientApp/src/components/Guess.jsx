import React from "react";

const Guess = ({ value }) => {

  const data = JSON.parse(value);
  const guess = data.Guess;
  const status = data.LetterStatus;

  return (
    <div className=" guessword ">
      {status.map((prop, index) => (
        <div key={index} className={`letter letter-${prop}`}>
          {guess[index].toUpperCase()}
        </div>
      ))}
    </div>
  );
};

export default Guess;
