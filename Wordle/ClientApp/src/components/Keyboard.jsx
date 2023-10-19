import React, { useEffect, useState } from 'react'


const Keyboard = ({ value }) => {

    const [attempts, setAttempts] = useState([]);
    const [arr, setArr] = useState([]);
    const [keys, setKeys] = useState([])

    useEffect(() => {
    
        setAttempts(value);
    }, [value]);

    useEffect(() => {
        setArr(attempts.map(JSON.parse));
    }, [attempts]);

    useEffect(() => {
        setKeys(["qwertyuiopå","asdfghjklöä","zxcvbnm"]);
    }, [arr]);

    const findLetterStatus = (attemptsArray, letterToFind) => {
        for (let i = 0; i < attemptsArray.length; i++) {
            const guess = attemptsArray[i].Guess;
            const letterStatus = attemptsArray[i].LetterStatus;

            if (guess.includes(letterToFind)) {
                // Assuming guess and letterStatus have the same length
                const index = guess.indexOf(letterToFind); 
                return letterStatus[index];
            }
        }
        return null; // Return null if the letter is not found in any guess
    };

    return (
        keys && <div className='keyboard'>
            {keys.map((row, rowIndex) => (
                <div key={rowIndex}>
                    {row.split('').map((letter, letterIndex) => (
                        findLetterStatus(arr, letter) === "1" ? <div className='letter l-small k-letter-1' key={letterIndex}>{letter}</div> :
                        findLetterStatus(arr, letter) === "2" ? <div className='letter l-small k-letter-2' key={letterIndex}>{letter}</div> :
                        findLetterStatus(arr, letter) === "3" ? <div className='letter l-small k-letter-3' key={letterIndex}>{letter}</div> :
                        <div className='letter l-small' key={letterIndex}>{letter} </div>
                    ))}
                </div>
            ))}
        </div>
    );
};

export default Keyboard;
