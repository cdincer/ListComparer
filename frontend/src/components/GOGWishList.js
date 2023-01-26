import {  useState } from "react";
import Container from 'react-bootstrap/Container';

export default function GOGWishList() {
 const [ GOGWishListGames, setGOGWishlist ] = useState([]);
 
const handleClick = async (event) => {
  event.preventDefault();
    const response = await fetch(
    'https://localhost:7181/GOG');
     const data = await response.json();
     setGOGWishlist(data);
     console.log('TO-DO:Add Performance Counter');

};
 
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
        <form onSubmit={handleClick}>
    <button type="submit" className="btn">Get GOG Wish List</button>    
    </form>
  </div>
</Container>
 );
}