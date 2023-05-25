import React from "react";
import Container from 'react-bootstrap/Container';
import { useState } from "react";

export default function Bargain() {
    const [FreeEpicGames, setEpicFreeList] = useState([]);

    const handleClick = async (event) => {
        event.preventDefault();

        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
        };
        const response = await fetch(
            'https://localhost:7181/Bargain', requestOptions);

        const data = await response.json();
        //    console.log(nums[right].Price.replace(/,/, "."));
        // for (let i = 0; i < data.length; i++) {
        //     data[i].Price = data[i].Price.replace(/,/, ".");
        // }

        // const newArray = QuickSort(data, 0, data.length - 1);
        setEpicFreeList(data);
        // let sum = SumOfWishList(newArray);

        console.log('TO-DO:Add Performance Counter');
    };




    return (
        <Container className="container">
            <div>
                <table className="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>Title </th>
                            <th>TimeStart</th>
                            <th>TimeEnd</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            FreeEpicGames.map((game, key) =>
                                <tr key={key}>
                                    <td>
                                        {game.Name}
                                    </td>
                                    <td>
                                        {game.TimeStart}
                                    </td>
                                    <td>
                                        {game.TimeEnd}
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
    )
};