import React, { useEffect, useState } from "react";
import Auth from './api-authorization/AuthorizeService'
import HighscoreAllTime from './HighscoreAllTime';
import HighscoreToday from './HighscoreToday';
import UserStats from './UserStats';

let config = { headers: {} }; //Request header to be filled with JWT token
let myToken;

const Profile = () => {

    const [highscore, setHighscore] = useState([[]]); // Nested arrays with top 10 today and all-time
    const [userStats, setUserStats] = useState([]); //Array with user average statistic data

    useEffect(() => {

        //Async function to fetch token
        const FetchDataWithToken = async () => {
            myToken = await Auth.getAccessToken()
            config = {
                headers: myToken ? { 'Authorization': `Bearer ${myToken}` } : {}
            };
            fetchDataProfile();
            fetchDataHighscore();
        };

        FetchDataWithToken();
    }, []);

    // Fetch logged in users statistic average data (uses token)
    const fetchDataProfile = async () => {
        try {
            myToken = await Auth.getAccessToken()
            config = {
                headers: myToken ? { 'Authorization': `Bearer ${myToken}` } : {}
            };
            const response = await fetch('/api/profile', config)
            const dataP = await response.json();
            setUserStats(dataP);
        } catch (error) {
            console.error('Error fetching profile data:', error);
        }
    }

    // Fetch global highscore top 10 today and all-time (dosen't use token)
    const fetchDataHighscore = async () => {
        try {
            const response = await fetch('/api/highscore')
            const dataH = await response.json();
            setHighscore(dataH);
        } catch (error) {
            console.error('Error fetching highscores:', error);
        }
    }

    return (
        <>
            {
                <div className='highscore-container'>
                    <div className='profile-score-field'>
                        <UserStats value={ userStats } />
                    </div>
                    <div className='profile-score-field'>
                        <HighscoreAllTime value={ highscore } />
                    </div>
                    <div className='profile-score-field'>
                        <HighscoreToday value={ highscore } />
                    </div>
                </div>
            }
        </>
    )
}

export default Profile