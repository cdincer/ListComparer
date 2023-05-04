import React from "react";
import Navbar2 from "react-bootstrap/Navbar"
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import { NavLink } from "react-router-dom";


const Navbar = () => {
  return (
    <Navbar2 className="navbar navbar-inverse" expand="lg">
      <Container className="container">
        <Navbar2.Brand className="navbar-brand" href="#home">List Comparer</Navbar2.Brand>
        <Nav className="me-auto">
          <Nav.Link href="/">Home</Nav.Link>
          <span>|</span><NavLink to="/library">Library</NavLink>
          <span>|</span><NavLink to="/about">About</NavLink>
        </Nav>
      </Container>
    </Navbar2>
  );
};

export default Navbar;
