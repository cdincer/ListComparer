import React from "react";
import { Link } from "react-router-dom";
import  ListGroup from "react-bootstrap/ListGroup";
import Container from 'react-bootstrap/Container';

const HomePage = () => {
  return(
    <>
    <Container className="container">

    <div className="col-sm">
    <ListGroup>
    <ListGroup.Item>Cras justo odio</ListGroup.Item>
    <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
    <ListGroup.Item>Morbi leo risus</ListGroup.Item>
    <ListGroup.Item>Porta ac consectetur ac</ListGroup.Item>
    <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
    </ListGroup>
    </div>
    <div className="col-sm">
      <ListGroup>
    <ListGroup.Item>Cras justo odio</ListGroup.Item>
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

export default HomePage;
