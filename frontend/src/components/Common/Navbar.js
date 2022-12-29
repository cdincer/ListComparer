import React from "react";
import Navbar2 from "react-bootstrap/Navbar"
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';


const Navbar = () => {
  return (
    <Navbar2 className="navbar navbar-inverse" expand="lg">
    <Container className="container">
    <Navbar2.Brand className="navbar-brand" href="#home">Navbar with text</Navbar2.Brand>     
      <Nav className="me-auto">
            <Nav.Link  href="#home">Home</Nav.Link>
            <span>|</span><Nav.Link  href="#link">Test-Link-1</Nav.Link>
            <span>|</span><Nav.Link  href="#link">Test-Link-2</Nav.Link>

            </Nav>
    </Container>
  </Navbar2>
  );
};

export default Navbar;
