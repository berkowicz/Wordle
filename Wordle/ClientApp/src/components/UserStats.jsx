import React from 'react'

const UserStats = ({ value }) => {

    return (
        <div>
            <h2>Your avg stats</h2>
            <p>
                {`Win percent: ${value.winPercent}% | Score: ${value.score} | Time: ${value.time}`}
            </p>
        </div>
    );
};

export default UserStats