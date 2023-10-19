import React from 'react';

const HighscoreToday = ({ value }) => {
    const today = value.highscoreToday; // Todays highscore array

    // Maps data and returns data to /profile
    return (
        <div className='profile-score-field'>
            <h2>Todays High Scores</h2>
            <ul className='highscore-ul'>
                {today && today.map((array, index) => (
                    <li className='highscore-li' key={index}>
                        {`#${index + 1} | Score: ${array.score} | Time: ${array.timer} | Date: ${array.date}`}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default HighscoreToday;