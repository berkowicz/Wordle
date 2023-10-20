import React from 'react';

const HighscoreToday = ({ value }) => {
    const today = value.highscoreToday; // Todays high score array

    return (
        <div className='profile-score-field'>
            <h2>Dagens high score</h2>
            <table className='highscore-table'>
                <thead>
                    <tr>
                        <th></th>
                        <th>Score</th>
                        <th>Tid (min)</th>
              
                    </tr>
                </thead>
                <tbody>
                    {today && today.map((array, index) => (
                        <tr key={index}>
                            <td>{index + 1}</td>
                            <td>{Math.round(array.score)}</td>
                            <td>{array.timer / 60}</td>
                  
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default HighscoreToday;
