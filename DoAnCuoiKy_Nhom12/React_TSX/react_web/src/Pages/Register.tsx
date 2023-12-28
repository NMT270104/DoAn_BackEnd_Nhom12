import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { toast } from 'react-toastify';
import axios from 'axios';
import { ToastContainer } from 'react-toastify';


const Register = () => {
  const [userName, userNamechange] = useState("");
  const [email, emailchange] = useState("");
  const [password, passwordchange] = useState("");

  const handlesubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    let regobj = { userName, email, password };

    try {
      const response = await axios.post('http://localhost:40080/Account/Register', regobj, {
        headers: {
          'Content-Type': 'application/json',
        },
      });

      toast.success("Đăng kí thành công.");
      console.log("Đăng kí thành công:", response.data);
    } catch (error) {
      toast.error("Đăng kí thất bại.");
      console.error("Đăng kí thất bại:", error);
    }
  };

  return (
    <>
    <ToastContainer></ToastContainer>
      <div className="container mt-5">
        <div className="row d-flex justify-content-center align-items-center">
          <div className="col-lg-4 bg-white m-auto">
            <h2 className="text-center">Sign up now</h2>
            <p className="text-center">To receive new products and sales</p>
            <form action="" onSubmit={handlesubmit}>
              <div className="input-group mb-3">
                <span className="input-group-text">
                  <i className="fa-solid fa-user" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Username"
                  value={userName}
                  onChange={(e) => userNamechange(e.target.value)}
                />
              </div>
              <div className="input-group mb-3">
                <span className="input-group-text">
                  <i className="fa-solid fa-lock" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Email"
                  value={email}
                  onChange={(e) => emailchange(e.target.value)}
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
                  value={password}
                  onChange={(e) => passwordchange(e.target.value)}
                  autoComplete="current-password"
                />
              </div>
              {/* <div className="input-group mb-3">
                <span className="input-group-text">
                  <i className="fa-solid fa-lock" />
                </span>
                <input
                  type="password"
                  className="form-control"
                  placeholder="Confirm Password"
                  onChange={(e) => passwordchange(e.target.value)}
                />
              </div> */}
              <div className="d-grid">
                <button type="submit" className="btn btn-primary">
                  Sign up
                </button>
                <p className="text-center mt-3">
                  When you register by clicking sign up button, you Agree to our
                  <a href="/#"> Terms and Conditions </a>
                  and
                  <a href="/#"> Privacy Policy</a>
                </p>
                <p className="text-center">
                  Already have an account?
                  <a href="/#"> Login here</a>
                </p>
              </div>
            </form>
          </div>
        </div>
      </div>
    </>
  );
}

export default Register;