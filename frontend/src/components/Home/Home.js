import React from "react";
import { Link } from "react-router-dom";

const HomePage = () => (
  <div className="jumbotron">
  <h1>Theme Testing</h1>
  <p>Theme Paragraph.</p>
  <Link to="about" className="btn btn-primary btn-lg">
   Learn more about bootstrap x386
  </Link>
</div>
);

export default HomePage;
