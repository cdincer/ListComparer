import {  useState } from "react";
import Container from 'react-bootstrap/Container';

export default function GOGWishList() {
 const [ GOGWishListGames, setGOGWishlist ] = useState([]);
 const [SumOfWishListedGOGGames,setSumOfWishListedGOGGames] = useState([]);

 
const handleClick = async (event) => {
  event.preventDefault();

  var optCarrier =  require('../../Addresses.json');
  var GOGWishListAddressVar="";

  for (var i = 0; i < optCarrier.length; i++)
  {
      var obj = optCarrier[i];
      if(obj.Name === 'GOGWishListAddress')
      {
        GOGWishListAddressVar =obj.Value;
        break;
      }
  }


  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ profileAddress: GOGWishListAddressVar })
};
    const response = await fetch(
    'https://localhost:7181/GOG',requestOptions);
     const data = await response.json();
     setGOGWishlist(data);
     let sum =0;
     for (let i = 0; i < data.length; i++) {
      let PriceVariable =0;
      if (Number.isInteger(parseInt(data[i].Price) )) {
        PriceVariable = parseFloat(data[i].Price.replace(',','.').replace(' ',''))
      }
      sum = sum + PriceVariable;
      setSumOfWishListedGOGGames(sum.toFixed(2));
  }
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
               {game.Name}
              </td>
              <td>
              {game.Price}
              </td>
            </tr>
         )
}
        <tr><td>Total of All Wishlisted GOG Games</td>
        <td>{SumOfWishListedGOGGames}</td>
        </tr>
          </tbody>
        </table>
        <form onSubmit={handleClick}>
    <button type="submit" className="btn">Get GOG Wish List</button>    
    </form>
  </div>
</Container>
 );
}