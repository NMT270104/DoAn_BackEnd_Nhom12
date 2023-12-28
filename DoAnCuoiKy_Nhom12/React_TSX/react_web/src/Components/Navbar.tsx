import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import { Link } from "react-router-dom";
import { Nav } from "react-bootstrap";


const Navbar = () => {
  return (
    <>
      <nav className="navbar navbar-expand-lg bg-body-tertiary">
        <div className="container-fluid">
          <a className="navbar-brand me-auto" href="/#">
          </a>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon" />
          </button>
          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav justify-content-center flex-grow-1 pe-3">
              <li className="nav-item">
                <Nav.Link as={Link} to={"/"} active>
                  Home
                </Nav.Link>
              </li>
              <li className="nav-item">
                <Nav.Link as={Link} to={"/about"} active>
                  About
                </Nav.Link>
              </li>
              <li className="nav-item">
                <Nav.Link as={Link} to={"/product"} active>
                  Product
                </Nav.Link>
              </li>
              <li className="nav-item">
                <Nav.Link as={Link} to={"/login"} active>
                  Login
                </Nav.Link>
              </li>
              <li className="nav-item">
                <Nav.Link as={Link} to={"/register"} active>
                  Register
                </Nav.Link>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </>
  );
}

export default Navbar;