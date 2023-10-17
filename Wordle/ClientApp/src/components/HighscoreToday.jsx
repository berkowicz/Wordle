import React from 'react';

const HighscoreToday = ({ value }) => {
    const today = value.highscoreToday;
    console.log(today)
    return (
        <div>
            <h2>Today High Scores</h2>
            <ul>
                {today && today.map((array, index) => (
                    <li key={index}>
                        {`${index + 1} | Score: ${array.score} | Time: ${array.timer} | Date: ${array.date}`}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default HighscoreToday;