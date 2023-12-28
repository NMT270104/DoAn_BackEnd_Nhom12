import React from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const MyComponent = () => {
  const notify = () => toast("Xin chào thế giới!");

  return (
    <div>
      <button onClick={notify}>Hiển thị Toast</button>
      <ToastContainer />
    </div>
  );
};

export default MyComponent;
