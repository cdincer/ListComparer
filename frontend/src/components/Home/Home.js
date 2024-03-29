import React from "react";
import Container from 'react-bootstrap/Container';
import SteamWishList from "../Wishlist/SteamWishList";
import GOGWishList from "../Wishlist/GOGWishList";

const HomePage = () => {

  var data =  require('../../Addresses.json');
  var SteamWishListAddressVar="";
  var GOGWishListAddressVar="";

  for (var i = 0; i < data.length; i++)
  {
      var obj = data[i];
      // console.log(`Name: ${obj.Name}, ${obj.Value}`);

      if(obj.Name === 'SteamWishListAddress')
      {
        SteamWishListAddressVar =obj.Value;
      }
      else if(obj.Name === 'GOGWishListAddress')
      {
        GOGWishListAddressVar =obj.Value;
      }
  }

  return(
    <>
    <Container className="container">
    <SteamWishList SteamWishListAddress={SteamWishListAddressVar}/>
    <br></br>
    <GOGWishList GOGWishListAddress={GOGWishListAddressVar}/>
</Container>
</>
)
  };

export default HomePage;
