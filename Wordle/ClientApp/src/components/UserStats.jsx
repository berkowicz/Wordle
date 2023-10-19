import React from 'react'

const UserStats = ({ value }) => {

    // Returns data to /profile
    return (
        <div className='profile-score-field'>
            <h2>Your average stats</h2>
            <p className='userStats-p'>
                {`Win percent: ${value.winPercent}%`}
            </p>
            <p className='userStats-p'>
                {`Avg score won games: ${value.score}`} 
            </p>
            <p className='userStats-p'>
                {`Avg time won games: ${value.time}`}
            </p>
        </div>
    );
};

export default UserStats