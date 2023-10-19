import React from 'react'
import { Link,
NavLink } from 'react-router-dom';

const GameOver = ({reset, correct }) => {
  return (
    <div className='finishedGame'>
        {correct ? <> Rätt ord:  <p> {correct}  </p> </> : "Du klarade det!" }

        
        <NavLink tag={Link} className="newgame" onClick={ reset  }>
      
            Nytt spel

        </NavLink>
   
    </div> 
  )
}

export default GameOver