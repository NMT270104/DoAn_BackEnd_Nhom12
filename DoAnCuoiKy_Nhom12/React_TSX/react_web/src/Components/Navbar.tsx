import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Link } from "react-router-dom";
import { Nav } from "react-bootstrap";

function Navbar() {
  return (
    <>
      <header className="header">
        <div className="container">
          <div className="row">
            <div className="col-xl-4 m-auto">
              <form className="d-flex" role="search">
                <input
                  className="form-control me-2"
                  type="search"
                  placeholder="Search"
                  aria-label="Search"
                />
                <button className="btn btn-outline-success" type="submit">
                  Search
                </button>
              </form>
            </div>

            <div className="col">
              <a href="/#">
                <i className="fa-solid fa-user"></i>
              </a>
            </div>
            <div className="col">
              <a href="/#">
                
              </a>
            </div>
          </div>
        </div>
      </header>
      <nav className="navbar navbar-expand-lg bg-body-tertiary">
        <div className="container-fluid">
          <a className="navbar-brand me-auto" href="/#">
            Navbar
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
                <a
                  className="nav-link active mx-lg-2"
                  aria-current="page"
                  href="/#"
                >
                  Home
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link mx-lg-2" href="/#">
                  About
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link mx-lg-2" href="/#">
                  Service
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link mx-lg-2" href="/#">
                  Portfolio
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link mx-lg-2" href="/#">
                  Contact
                </a>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </>
  );
}

export default Navbar;
