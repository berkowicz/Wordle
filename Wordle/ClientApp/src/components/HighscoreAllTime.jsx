import React from 'react';

const HighscoreAllTime= ({ value }) => {
    const allTime = value.highscoreAllTime;

    return (
        <div>
            <h2>All Time High Scores</h2>
            <ul>
                {allTime && allTime.map((array, index) => (
                    <li key={index}>
                        {`${index + 1} | Score: ${array.score} | Time: ${array.timer} | Date: ${array.date}`}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default HighscoreAllTime;