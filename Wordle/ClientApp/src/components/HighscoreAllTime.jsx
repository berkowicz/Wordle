import React from 'react';

const HighscoreAllTime = ({ value }) => {
    const allTime = value.highscoreAllTime; // All-time highscore array

    console.log(value)
    // Function to format the date and time
    const formatDateTime = (dateTimeString) => {
        const options = {
            year: 'numeric',
            month: 'short',
            day: '2-digit',
            hour12: false,
        };
        return new Intl.DateTimeFormat('sv', options).format(new Date(dateTimeString));
    };

    return (
        <div className='profile-score-field'>
            <h2>High score</h2>
            <table className='highscore-table'>
                <thead>
                    <tr>
                        <th></th>
                        <th>Försök</th>
                        <th>Tid</th>
                        <th>Datum</th>
                    </tr>
                </thead>
                <tbody>
                    {allTime && allTime.map((array, index) => (
                        <tr key={index}>
                            <td>{index + 1}</td>
                            <td>{array.score}</td>
                            <td>{array.timer}</td>
                            <td>{formatDateTime(array.date).split(",")[0]}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default HighscoreAllTime;
