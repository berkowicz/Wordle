import React, { useEffect, useState } from "react";
import Auth from './api-authorization/AuthorizeService'
import HighscoreAllTime from './HighscoreAllTime';
import HighscoreToday from './HighscoreToday';
import UserStats from './UserStats';

let config = { headers: {} }; //Request header to be filled with JWT token
let myToken;

//const apiHost = '/api/Profile';


const Profile = () => {

    const [highscore, setHighscore] = useState([[]]);
    const [userStats, setUserStats] = useState([]);

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
                <div>
                    <div>
                        <HighscoreAllTime value={ highscore } />
                    </div>
                    <div>
                        <HighscoreToday value={ highscore } />
                    </div>
                    <div>
                        <UserStats value={ userStats } />
                    </div>
                </div>
            }
        </>
    )
}

export default Profile