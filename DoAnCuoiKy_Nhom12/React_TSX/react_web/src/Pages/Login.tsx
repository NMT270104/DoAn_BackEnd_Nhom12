import React, { useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { toast } from "react-toastify";
import axios from "axios";
import { ToastContainer } from "react-toastify";

const Login = () => {
  const [userName, userNamelogin] = useState("");
  const [password, passwordlogin] = useState("");

  const handlesubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    let regobj = { userName, password };

    try {
      const response = await axios.post(
        "https://localhost:40443/Account/Login",
        regobj,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      toast.success("Đăng nhập thành công.");
      console.log("Đăng nhập thành công:", response.data);
    } catch (error) {
      toast.error("Đăng nhập thất bại.");
      console.error("Đăng nhập thất bại:", error);
    }
  };

  return (
    <>
    <ToastContainer></ToastContainer>
      <div className="container mt-5">
        <div className="row d-flex justify-content-center align-items-center">
          <div className="col-lg-4 bg-white m-auto">
            <h2 className="text-center">Login now</h2>
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
                  onChange={(e)=> userNamelogin(e.target.value)}
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
                  onChange={(e)=>passwordlogin(e.target.value)}
                />
              </div>
              <div className="d-grid">
                <button type="submit" className="btn btn-primary">
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
};

export default Login;
