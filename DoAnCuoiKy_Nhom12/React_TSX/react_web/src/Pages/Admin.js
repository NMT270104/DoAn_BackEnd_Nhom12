import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";

const Admin = () => {
  const [username, usernameUpdate] = useState("");
  const [password, PasswordUpdate] = useState("");

  const usenavigate = useNavigate();

  const ProceedLogin = (e) => {
    e.preventDefault();
    if (validate()) {
      //console.log("success");
      fetch("http://localhost:40080/Account/Register" + username)
        .then((res) => {
          return res.json();
        })
        .then((resp) => {
          if (Object.keys(resp).length === 0) {
            toast.error("Sai tên đăng nhập");
          } else {
            if (resp.password === password) {
              toast.success("Đăng nhập thành công");
              usenavigate("/");
            } else {
              toast.error("Sai mật khẩu");
            }
          }
        })
        .catch((err) => {
          toast.error("Đăng nhập thất bại");
        });
    }
  };

  const validate = () => {
    let result = true;
    if (username === "" || username === null) {
      return false;
      toast.warning("Hãy nhập tên");
    }
    if (password === "" || password === null) {
      return false;
      toast.warning("Hãy nhập mật khẩu");
    }
    return result;
  };
  return (
    <>
      <div className="container mt-5">
        <div className="row d-flex justify-content-center align-items-center">
          <div className="col-lg-4 bg-white m-auto">
            <h2 className="text-center">Admin</h2>
            <form onSubmit={ProceedLogin}>
              <div className="input-group mb-3">
                <span className="input-group-text">
                  <i className="fa-solid fa-lock" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Username"
                  value={username}
                  onChange={(e) => usernameUpdate(e.target.value)}
                />
              </div>
              <div className="input-group mb-3">
                <span className="input-group-text">
                  <i className="fa-solid fa-lock" />
                </span>
                <input
                  type="password"
                  className="form-control"
                  placeholder="Enter Password"
                  value={password}
                  onChange={(e) => PasswordUpdate(e.target.value)}
                />
              </div>
              <div className="d-grid">
                <button type="submit" className="btn btn-primary">
                  Login
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </>
  );
};

export default Admin;
