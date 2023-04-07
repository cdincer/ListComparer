import { useState } from "react";
import Container from 'react-bootstrap/Container';

export default function GOGWishList() {
  const [GOGWishListGames, setGOGWishlist] = useState([]);
  const [SumOfWishListedGOGGames, setSumOfWishListedGOGGames] = useState([]);


  const handleClick = async (event) => {
    event.preventDefault();

    var optCarrier = require('../../Addresses.json');
    var GOGWishListAddressVar = "";

    for (var i = 0; i < optCarrier.length; i++) {
      var obj = optCarrier[i];
      if (obj.Name === 'GOGWishListAddress') {
        GOGWishListAddressVar = obj.Value;
        break;
      }
    }


    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ profileAddress: GOGWishListAddressVar })
    };
    const response = await fetch(
      'https://localhost:7181/GOG', requestOptions);
    const data = await response.json();

    //Double TBA check for thoroughness WIP
    for (let i = 0; i < data.length; i++) {
      if (data[i].Price === 'TBA') {
        const obj = { Name: data[i].Name, Price: 'TBA' };
        data.splice(i, 1);
        data.push(obj);
      }
    }

    const newArray = QuickSort(data, 0, data.length - 1);

    //Double TBA check for thoroughness WIP
    for (let i = 0; i < newArray.length; i++) {
      if (newArray[i].Price === 'TBA') {
        const obj = { Name: newArray[i].Name, Price: 'TBA' };
        newArray.splice(i, 1);
        newArray.push(obj);
      }
    }

    setGOGWishlist(newArray);
    let sum = SumOfWishList(data);
    setSumOfWishListedGOGGames(sum.toFixed(2));
    console.log('TO-DO:Add Performance Counter');

  };

  //TO-DO:Will be implementing quick sort and slimming down the parseFloat bloat.
  function QuickSort(nums, left, right) {
    if (left < right) {
      let pivotIndex = Partition(nums, left, right);
      QuickSort(nums, left, pivotIndex - 1);
      QuickSort(nums, pivotIndex + 1, right);
    }
    return nums;
  }

  function Partition(nums, left, right) {
    let pivot = Math.round(parseFloat(nums[right].Price) * 100000); //required for dealing with decimals 
    let pivotIndex = left;

    for (let i = left; i < right; i++) {
      if (Math.round(parseFloat(nums[i].Price) * 100000) <= pivot) {
        Swap(nums, i, pivotIndex);
        pivotIndex++;
      }
    }

    Swap(nums, pivotIndex, right);
    return pivotIndex;
  }

  function Swap(nums, i, j) {
    let temp = nums[i];
    nums[i] = nums[j];
    nums[j] = temp;
  }

  function SumOfWishList(WTBS) {
    let sum = 0;
    for (let i = 0; i < WTBS.length; i++) {
      let PriceVariable = 0;
      if (Number.isInteger(parseInt(WTBS[i].Price))) {
        PriceVariable = parseFloat(WTBS[i].Price.replace(',', '.').replace(' ', ''))
      }
      sum = sum + PriceVariable;
    }
    return sum;
  }

  return (
    <Container className="container">
      <div className="col-sm">
        <table className="table table-bordered table-striped">
          <thead>
            <tr>
              <th>Title</th>
              <th>Price</th>
            </tr>
          </thead>
          <tbody>

            {
              GOGWishListGames.map((game, key) =>
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
            <tr><td>Wishlist Count:{GOGWishListGames.length}</td>
              <td>Total:{SumOfWishListedGOGGames}</td>
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