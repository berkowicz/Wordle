import React from 'react'

const Guess = ({ value }) => {

const data = JSON.parse(value);
const guess = data.Guess;
const status = data.LetterStatus;
console.log(guess)
console.log(status)


  return (
    <div className=' guessword '>

{    
status.map((prop, index) => (
        <div key={index} className={`letter-${prop}`}> 
            {guess[index]}
        </div>

      ))
      }
    </div>
  )
}

export default Guess