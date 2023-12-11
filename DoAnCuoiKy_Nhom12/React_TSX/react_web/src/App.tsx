import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './Pages/Home';
import Navbar from './Components/Navbar';
import About from './Pages/About';
import Product from './Pages/Product';

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path='/*' element={
            <>
            <Navbar></Navbar>
            <Routes>
              <Route path='/' element={<Home></Home>}></Route>
              <Route path='/product' element={<Product></Product>}></Route>
              <Route path='/about' element={<About></About>}></Route>
            </Routes>
            </>
          }></Route>
        </Routes>
      </Router>
    </div>
  );
}

export default App;
