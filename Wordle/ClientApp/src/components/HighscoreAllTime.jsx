import React from 'react';

const HighscoreAllTime= ({ value }) => {
    const allTime = value.highscoreAllTime; // All-time highscore array

    // Maps data and returns data to /profile
    return (
        <div className='profile-score-field'>
            <h2>All Time High Scores</h2>
            <ul className='highscore-ul'>
                {allTime && allTime.map((array, index) => (
                    <li className='highscore-li' key={index}>
                        {`#${index + 1} | Score: ${array.score} | Time: ${array.timer} | Date: ${array.date}`}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default HighscoreAllTime;