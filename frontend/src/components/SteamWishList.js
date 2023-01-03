import React from "react";
import  ListGroup from "react-bootstrap/ListGroup";
import Container from 'react-bootstrap/Container';

function SteamWishList(props) {  
    return(
    <>
    <Container className="container">
    <div className="col-sm">
    <ListGroup>
    <ListGroup.Item>I am the test SteamWishList {props.SteamWishListAddress}</ListGroup.Item>
    <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
    <ListGroup.Item>Morbi leo risus</ListGroup.Item>
    <ListGroup.Item>Porta ac consectetur ac</ListGroup.Item>
    <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
    </ListGroup>
    </div>
</Container>
</>
)
  };

export default SteamWishList;
