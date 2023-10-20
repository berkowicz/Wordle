import React from 'react'

const UserStats = ({ value }) => {

    const winPercent = Math.round(value.winPercent)
    const attempts = Math.round(value.score)
    const timeSpent = Math.round(value.time/60)

    // Returns data to /profile
    return (
        <div className='profile-score-field'>
            <h2>Din statistik</h2>
            <b className='userStats-p'>
                {`Andel vunna: ${winPercent}%`}
            </b>
            <p className='userStats-p'>
                {`Snitt antal försök: ${attempts}`} 
            </p>
            <p className='userStats-p'>
                {`Snitt spenderad tid: ${timeSpent} min`}
            </p>
        </div>
    );
};

export default UserStats