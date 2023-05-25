import './App.css';
import './bootstrap/css/bootstrap.min.css';
import { Routes, Route } from "react-router-dom";
import HomePage from './components/Home/Home';
import Navbar from './components/Common/Navbar';
import Library from './components/Library/Library';
import About from './components/About/About';
import React from "react";
import Bargain from './components/Bargain/Bargain';

function App() {
   return (
      <div>
         <Navbar />
         <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/library" element={<Library />} />
            <Route path="/bargain" element={<Bargain />} />
            <Route path="/about" element={<About />} />
         </Routes>
      </div>
   );
}

export default App;
