import './App.css';
import './bootstrap/css/bootstrap.min.css';
import { Routes, Route} from "react-router-dom";
import HomePage  from './components/Home/Home';
import Navbar  from './components/Common/Navbar';
import React from "react";

function App() {
  return (
     <div>
        <Navbar />
        <Routes>
        <Route path="/" element={<HomePage />} />
        </Routes>
     </div>
  );
}

export default App;
