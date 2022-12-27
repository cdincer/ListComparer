import React from "react";

const Navbar = () => {
  return (
    <div className ="navbar navbar-inverse navbar-fixed-top">
    <div className ="navbar-inner">
      <div className ="container">
        <button type="button" className ="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
          <span className ="icon-bar"></span>
          <span className ="icon-bar"></span>
          <span className ="icon-bar"></span>
        </button>
        <a className ="brand" href="https://www.w3schools.com/">Project name</a>
        <div className ="nav-collapse collapse">
          <ul className ="nav">
            <li className ="active"><a href="https://www.w3schools.com/">Home</a></li>
            <li><a href="#about">About</a></li>
            <li><a href="#contact">Contact</a></li>
          </ul>
        </div>
      </div>
    </div>
  </div>
  );
};

export default Navbar;
