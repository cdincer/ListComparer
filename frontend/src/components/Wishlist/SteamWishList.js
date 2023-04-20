import { useState } from "react";
import Container from 'react-bootstrap/Container';

export default function SteamWishList() {
  const [SteamWishListGames, setSteamWishList] = useState([]);
  const [SumOfWishListedSteamGames, setSumOfWishListedSteamGames] = useState([]);

  const handleClick = async (event) => {
    event.preventDefault();

    var optCarrier = require('../../Addresses.json');
    var SteamWishListAddressVar = "";

    for (var i = 0; i < optCarrier.length; i++) {
      var obj = optCarrier[i];
      if (obj.Name === 'SteamWishListAddress') {
        console.log(obj.Value);
        SteamWishListAddressVar = obj.Value;
        break;
      }
    }

    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ profileAddress: SteamWishListAddressVar })
    };
    const response = await fetch(
      'https://localhost:7181/Steam', requestOptions);

    const data = await response.json();

    const newArray = QuickSort(data, 0, data.length - 1);
    setSteamWishList(newArray);
    let sum = SumOfWishList(newArray);
    setSumOfWishListedSteamGames(sum.toFixed(2));

    console.log('TO-DO:Add Performance Counter');
  };

  //TO-DO:Slimming down the parseFloat bloat.
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
      <div>
        <table className="table table-bordered table-striped">
          <thead>
            <tr>
              <th>Title </th>
              <th>Price</th>
              <th>Publisher</th>
            </tr>
          </thead>
          <tbody>

            {
              SteamWishListGames.map((game, key) =>
                <tr key={key}>
                  <td>
                    {game.Title}
                  </td>
                  <td>
                    {game.Price}
                  </td>
                  <td>
                    {game.Publisher}
                  </td>
                </tr>
              )
            }
            <tr><td>Wishlist Count:{SteamWishListGames.length}</td>
              <td>Total:{SumOfWishListedSteamGames}</td>
              <td></td>
            </tr>
          </tbody>
        </table>
        <form onSubmit={handleClick}>
          <button type="submit" className="btn">Get Steam WishList</button>
        </form>
      </div>
    </Container>
  );
}