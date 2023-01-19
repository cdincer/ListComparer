import { useEffect, useState } from "react";
import Container from 'react-bootstrap/Container';

export default function GOGWishList() {
 const [ GOGWishListGames, setGOGWishlist ] = useState([]);
 
 useEffect(() => {
   const fetchata = async () => {
 
       const response = await fetch(
         'https://localhost:7181/GOG');
          const data = await response.json();

          //use only 3 sample data
          setGOGWishlist( data)
      
   }
 
   // Call the function
   fetchata();
}, []);


function HandleSave(event) {
  event.preventDefault()
  fetch("https://localhost:7181/GOG",
  {
    crossDomain:true
  })
    .then((response) => response.json())
    .then((responseJson) => {
      console.log(responseJson)
    })
}
 
 return (
  <Container className="container">
  <div className="col-sm">
  <table className="table table-bordered table-striped">
          <thead>
            <tr>
              <th>Name</th>
              <th>Price</th>
            </tr>
          </thead>
          <tbody>
       
         {
         GOGWishListGames.map( (game,key) =>
            <tr key={key}>
              <td>
                <span className="label">{game.Name}</span>
              </td>
              <td>
              {game.Price}
              </td>
            </tr>
         )
}
        
          </tbody>
        </table>
        <form onSubmit={HandleSave}>
    <button type="submit" className="btn">Get WishList</button>    
    </form>
  </div>
</Container>
 );
}