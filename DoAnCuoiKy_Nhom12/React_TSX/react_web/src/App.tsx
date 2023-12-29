import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './Pages/Home';
import Navbar from './Components/Navbar';
import About from './Pages/About';
import Product from './Pages/Product';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Admin from './Pages/Admin';
import MyComponent from './Pages/MyComponent';
import CRUD from './CRUD';
import CreateAuthorName from './Pages/CreateAuthorName';
import CreateCategory from './Pages/CreateCategory';
import CreateBook from './Pages/CreateBook';

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route
            path="/*"
            element={
              <>
                <Navbar></Navbar>
                <Routes>
                  <Route path="/" element={<Home></Home>}></Route>
                  <Route path="/product" element={<Product></Product>}></Route>
                  <Route path="/about" element={<About></About>}></Route>
                  <Route path="/product" element={<Product></Product>}></Route>
                  <Route path="/login" element={<Login></Login>}></Route>
                  <Route
                    path="/register"
                    element={<Register></Register>}
                  ></Route>
                </Routes>
              </>
            }
          ></Route>
          <Route path="/admin" element={<Admin></Admin>}></Route>
          <Route
            path="/mycomponent"
            element={<MyComponent></MyComponent>}
          ></Route>
          <Route path="/crud" element={<CRUD></CRUD>}></Route>
          <Route path="/createauthorname" element={<CreateAuthorName></CreateAuthorName>}></Route>
          <Route path="/createcategory" element={<CreateCategory></CreateCategory>}></Route>
          <Route path="/createbook" element={<CreateBook></CreateBook>}></Route>
        </Routes>
      </Router>
    </div>
  );
}

export default App;
