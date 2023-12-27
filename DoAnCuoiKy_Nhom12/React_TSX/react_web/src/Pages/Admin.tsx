import React from "react";

function Admin() {
    return(
        <>
        <div>
  <h2 className="text-center mt-5">Admin Page</h2>
  <div className="container mt-5">
    <div className="row justify-content-center">
      <div className="col-lg-6">
        <div className="input-group mb-3">
          <span className="input-group-text"><i className="fa-solid fa-user" /></span>
          <input type="text" className="form-control" placeholder="Nhập tên sách" />
        </div>
        <div className="input-group mb-3">
          <select className="form-select">
            <option selected value="">Chọn thể loại</option>
            <option value={1}>Truyện tranh</option>
            <option value={2}>Tiểu thuyết</option>
            <option value={3}>Chuyển sinh</option>
            <option value={4}>Xuyên không</option>
            <option value={5}>Trinh thám</option>
            <option value={6}>Hệ thống</option>
          </select>
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text"><i className="fa-solid fa-money-bill" /></span>
          <input type="text" className="form-control" placeholder="Nhập giá tiền" />
        </div>
        <div className="input-group mb-3">
          <select className="form-select">
            <option selected value="">Tên tác giả</option>
            <option value={1}>Đinh Quốc Hưng</option>
            <option value={2}>Nguyễn Minh Thư</option>
            <option value={3}>Nguyễn Châu Trường Giang</option>
          </select>
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text"><i className="fa-solid fa-box" /></span>
          <input type="text" className="form-control" placeholder="Nhập số lượng" />
        </div>
        <div className="d-flex justify-content-center align-items-center">
          <button className="btn btn-primary" type="button">Submit</button>
        </div>
      </div>
    </div>
  </div>
</div>
        </>
    )
}

export default Admin;
