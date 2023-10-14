import React from 'react'

const Guess = ({ value }) => {

    console.log("value")
console.log(value)

const data = JSON.parse(value);
const guess = data.Guess;
const status = data.LetterStatus;
console.log(guess)
console.log(status)


  return (
    <div className=' guessword '>

{    
status.map((prop, index) => (
        <div key={index} className={`letter letter-${prop}`}> 
            {guess[index].toUpperCase()}
        </div>

      ))
      }
    </div>
  )
}

export default Guess