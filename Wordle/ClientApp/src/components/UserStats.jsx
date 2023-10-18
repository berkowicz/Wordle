import React from 'react'

const UserStats = ({ value }) => {

    return (
        <div className='profile-score-field'>
            <h2>Your avg stats</h2>
            <p className='userStats-p'>
                {`Win percent: ${value.winPercent}%`}
            </p>
            <p className='userStats-p'>
                {`Score: ${value.score}`} 
            </p>
            <p className='userStats-p'>
                {`Time: ${value.time}`}
            </p>
        </div>
    );
};

export default UserStats