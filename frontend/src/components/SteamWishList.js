import {  useState } from "react";
import Container from 'react-bootstrap/Container';

export default function SteamWishList() {
 const [ SteamWishListGames, setSteamWishList ] = useState([]);
 
const handleClick = async (event) => {
  event.preventDefault();
    const response = await fetch(
    'https://localhost:7181/Steam');
     const data = await response.json();
     setSteamWishList(data);
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
         SteamWishListGames.map( (game,key) =>
            <tr key={key}>
              <td>
                <span className="label">{game.appid}</span>
              </td>
              <td>
              {game.added}
              </td>
            </tr>
         )
}
        
          </tbody>
        </table>
        <form onSubmit={handleClick}>
    <button type="submit" className="btn">Get Steam WishList</button>    
    </form>
  </div>
</Container>
 );
}