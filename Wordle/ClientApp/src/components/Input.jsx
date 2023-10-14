import React from 'react'

const Input = ({ value = "" }) => {

    const length = value.length
    console.log(length)

  return (
    

  <>
 
  {
    value.split('').map((item, index) => (
      <div key={index} className="letter">{item.toUpperCase()}</div>
    ))
  }
  
  {
         
        Array(5 - length).fill(null).map((_, index) => (
             <div key={index} className="letter"></div>
             ))
            
             
  }
            
            
            </>

       
            
    
  )
}

export default Input