import React from 'react'
import "bootstrap/dist/css/bootstrap.min.css";

function Login() {
  return (
    <>
      <div className="container mt-5">
        <div className="row d-flex justify-content-center align-items-center">
          <div className="col-lg-4 bg-white m-auto">
            <h2 className="text-center">Login now</h2>
            <p className="text-center">To receive new products and sales</p>
            <form action="" method="post">
              <div className="input-group mb-3">
                <span className="input-group-text">
                  <i className="fa-solid fa-lock" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Email"
                />
              </div>
              <div className="input-group mb-3">
                <span className="input-group-text">
                  <i className="fa-solid fa-lock" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Password"
                />
              </div>
              <div className="d-grid">
                <button type="button" className="btn btn-primary">
                  Login
                </button>
                <p className="text-center mt-3">
                  Don't have an account?
                  <a className="" href="/#">
                    Register
                  </a>
                </p>
              </div>
            </form>
          </div>
        </div>
      </div>
    </>
  );
}

export default Login;