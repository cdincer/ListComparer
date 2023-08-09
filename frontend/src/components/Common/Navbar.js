import React from "react";
import Navbar2 from "react-bootstrap/Navbar"
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import { NavLink } from "react-router-dom";

//https://stackoverflow.com/questions/72091197/what-is-the-difference-between-nav-link-vs-link-vs-navlink-in-react-router-dom-a
const Navbar = () => {
  return (
    <Navbar2 className="navbar navbar-inverse" expand="lg">
      <Container className="container">
        <Navbar2.Brand className="navbar-brand" href="#home">List Comparer</Navbar2.Brand>
        <Nav className="me-auto">
          <NavLink to="/">Home</NavLink>
          <span>|</span><NavLink to="/bargain">Bargains</NavLink>
          <span>|</span><NavLink to="/library">Library</NavLink>
          <span>|</span><NavLink to="/about">About</NavLink>
        </Nav>
      </Container>
    </Navbar2>
  );
};

export default Navbar;
