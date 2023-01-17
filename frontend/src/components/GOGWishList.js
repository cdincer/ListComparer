import React from "react";
import ListGroup from "react-bootstrap/ListGroup";
import Container from 'react-bootstrap/Container';


function HandleSave(event) {
  event.preventDefault()
  fetch("https://localhost:7181/GOG")
    .then((response) => response.json())
    .then((json) => {
      console.log(json)
    })
}

function GOGWishList() {  

 


    return(
    <>
    <Container className="container">
    <div className="col-sm">
    <ListGroup>
    <ListGroup.Item>I am the test GOGWishList </ListGroup.Item>
    <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
    <ListGroup.Item>Morbi leo risus</ListGroup.Item>
    <ListGroup.Item>Porta ac consectetur ac</ListGroup.Item>
    <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
    </ListGroup>
    <form onSubmit={HandleSave}>
    <button type="submit" className="btn">Get WishList</button>    
    </form>
    </div>
</Container>
</>
)
  };

export default GOGWishList;
