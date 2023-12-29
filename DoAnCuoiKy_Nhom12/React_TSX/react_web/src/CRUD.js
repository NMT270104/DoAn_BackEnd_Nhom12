import React, { useState, useEffect, Fragment } from "react";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import axios from "axios";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { Link } from "react-router-dom";

const CRUD = () => {

   const [show, setShow] = useState(false);

   const handleClose = () => setShow(false);
   const handleShow = () => setShow(true);

  return (
    // <Fragment>
    //   <Container className="mt-5">
    //     <Row className="justify-content-center">
    //       <Col className="col-lg-6">
    //         <div className="input-group mb-3">
    //           <span className="input-group-text">
    //             <i className="fa-solid fa-book"></i>
    //           </span>
    //           <input
    //             type="text"
    //             className="form-control"
    //             placeholder="Nhập tên sách"
    //           />
    //         </div>
    //         <div className="input-group mb-3">
    //           <span className="input-group-text">
    //             <i className="fa-solid fa-user" />
    //           </span>
    //           <input
    //             type="text"
    //             className="form-control"
    //             placeholder="Nhập tên tác giả"
    //           />
    //         </div>
    //         <div className="input-group mb-3">
    //           <select className="form-select">
    //             <option selected value="">
    //               Chọn thể loại
    //             </option>
    //             <option value={1}>Truyện tranh</option>
    //             <option value={2}>Tiểu thuyết</option>
    //             <option value={3}>Chuyển sinh</option>
    //             <option value={4}>Xuyên không</option>
    //             <option value={5}>Trinh thám</option>
    //             <option value={6}>Hệ thống</option>
    //           </select>
    //         </div>
    //         <div className="input-group mb-3">
    //           <span className="input-group-text">
    //             <i className="fa-solid fa-money-bill" />
    //           </span>
    //           <input
    //             type="text"
    //             className="form-control"
    //             placeholder="Nhập giá tiền"
    //           />
    //         </div>
    //         <div className="input-group mb-3">
    //           <span className="input-group-text">
    //             <i className="fa-solid fa-box" />
    //           </span>
    //           <input
    //             type="text"
    //             className="form-control"
    //             placeholder="Nhập số lượng"
    //           />
    //         </div>
    //         <div className="d-flex justify-content-center align-items-center">
    //           <button className="btn btn-primary" type="button">
    //             Submit
    //           </button>
    //         </div>
    //       </Col>
    //     </Row>
    //   </Container>

    //   <br></br>
    //   <Table striped bordered hover>
    //     <thead>
    //       <tr>
    //         <th>#</th>
    //         <th>BookID</th>
    //         <th>Tên sách</th>
    //         <th>Tên tác giả</th>
    //         <th>Thể loại</th>
    //         <th>Giá tiền</th>
    //         <th>Số lượng</th>
    //       </tr>
    //     </thead>
    //     <tbody>

    //     </tbody>
    //   </Table>

    //   <Modal show={show} onHide={handleClose}>
    //     <Modal.Header closeButton>
    //       <Modal.Title>Chỉnh sửa / Cập nhật</Modal.Title>
    //     </Modal.Header>
    //     <Modal.Body>
    //       <Container className="mt-3">
    //         <Row className="justify-content-center">
    //           <Col>
    //             <div className="input-group mb-3">
    //               <span className="input-group-text">
    //                 <i className="fa-solid fa-book"></i>
    //               </span>
    //               <input
    //                 type="text"
    //                 className="form-control"
    //                 placeholder="Nhập tên sách"
    //               />
    //             </div>
    //             <div className="input-group mb-3">
    //               <span className="input-group-text">
    //                 <i className="fa-solid fa-user" />
    //               </span>
    //               <input
    //                 type="text"
    //                 className="form-control"
    //                 placeholder="Nhập tên tác giả"
    //               />
    //             </div>
    //             <div className="input-group mb-3">
    //               <select className="form-select">
    //                 <option selected value="">
    //                   Chọn thể loại
    //                 </option>
    //                 <option value={1}>Truyện tranh</option>
    //                 <option value={2}>Tiểu thuyết</option>
    //                 <option value={3}>Chuyển sinh</option>
    //                 <option value={4}>Xuyên không</option>
    //                 <option value={5}>Trinh thám</option>
    //                 <option value={6}>Hệ thống</option>
    //               </select>
    //             </div>
    //             <div className="input-group mb-3">
    //               <span className="input-group-text">
    //                 <i className="fa-solid fa-money-bill" />
    //               </span>
    //               <input
    //                 type="text"
    //                 className="form-control"
    //                 placeholder="Nhập giá tiền"
    //               />
    //             </div>
    //             <div className="input-group mb-3">
    //               <span className="input-group-text">
    //                 <i className="fa-solid fa-box" />
    //               </span>
    //               <input
    //                 type="text"
    //                 className="form-control"
    //                 placeholder="Nhập số lượng"
    //               />
    //             </div>
    //             <div className="d-flex justify-content-center align-items-center">
    //               <button className="btn btn-primary" type="button">
    //                 Submit
    //               </button>
    //             </div>
    //           </Col>
    //         </Row>
    //       </Container>
    //     </Modal.Body>
    //     <Modal.Footer>
    //       <Button variant="secondary">
    //         Close
    //       </Button>
    //       <Button variant="primary">
    //         Save Changes
    //       </Button>
    //     </Modal.Footer>
    //   </Modal>
    // </Fragment>
    <>
    
    </>
  );
};

export default CRUD;
