import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import { Fragment } from "react";
import Table from "react-bootstrap/Table";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import axios from "axios";






const CreateAuthorName = () => {

  const empdata = [
    // {
    //   AuthorID: 1,
    //   AuthorName: "Hung",
    // },
  ];

  const [data, setData] = useState(empdata);
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [AuthorName,setAuthorName] = useState('');

  const [editAuthorName,seteditAuthorName] = useState('');
  const [editAuthorID, seteditAuthorID] = useState('');

  const navigate = useNavigate();

  const handleEdit =(AuthorID)=>{
    //alert(AuthorID);
    handleShow();
  }

  const handleDelete =(AuthorID)=>{
    if(window.confirm("Bạn có muốn xóa tác giả này chứ")== true){
          alert(AuthorID);
    }

  }

  const handleUpdate=()=>{

  }

  useEffect(() => {


    let jwttoken =
      "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4xMjMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbmlzdHJhdG9yIiwiZXhwIjoxNzAzODA5NDY4LCJpc3MiOiJXZWJBUEkiLCJhdWQiOiJXZWJBUEkifQ.2moV0hVhDeHeoeRmE8HoGrcq12eFQkTJeog9IeJVvKw";

    axios
      .get("https://localhost:40443/api/Admin/GetAuthors", {
        headers: {
          Authorization: "bearer " + jwttoken,
        },
      })
      .then((response) => {
        console.log(response.data);
        setData(response.data);
      })
      .catch((error) => {
        console.error("Error fetching data:", error.response);
      }); 
  }, [navigate]);

  // const getData =() =>{
  //   axios.get("https://localhost:40443/api/Admin/GetAuthors")
  //   .then((result)=>{
  //     setData(result.data)
  //   })
  //   .catch((error)=>{
  //     console.log(error);
  //   })
  // }

  return (
    <>
      <Fragment>
        <Container>
          <Row className="justify-content-center">
            <Col className="col-lg-6">
              <div className="input-group mb-3">
                <span className="input-group-text">
                  <i className="fa-solid fa-user"></i>
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Nhập tên tác giả"
                  value={AuthorName}
                  onChange={(e) => setAuthorName(e.target.value)}
                />
              </div>
            </Col>
            <Col>
              <button className="btn btn-primary" type="submit">
                Submit
              </button>
            </Col>
          </Row>
        </Container>
        <Table striped bordered hover size="sm">
          <thead>
            <tr>
              <th>#</th>
              <th>AuthorID</th>
              <th>AuthorName</th>
            </tr>
          </thead>
          <tbody>
            {data && data.length > 0
              ? data.map((item, index) => {
                  return (
                    <tr key={index}>
                      <td>{index + 1}</td>
                      <td>{item.AuthorID}</td>
                      <td>{item.AuthorName}</td>
                      <td colSpan={2}>
                        <button
                          className="btn btn-primary"
                          onClick={() => handleEdit(item.AuthorID)}
                        >
                          Edit
                        </button>{" "}
                        &nbsp;
                        <button
                          className="btn btn-danger"
                          onClick={() => handleDelete(item.AuthorID)}
                        >
                          Delete
                        </button>
                      </td>
                    </tr>
                  );
                })
              : "Loading..."}
          </tbody>
        </Table>
        <Modal show={show} onHide={handleClose}>
          <Modal.Header closeButton>
            <Modal.Title>Chỉnh sửa / Cập nhật tác giả</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Row className="justify-content-center">
              <Col className="col-lg-6">
                <div className="input-group mb-3">
                  <span className="input-group-text">
                    <i className="fa-solid fa-user"></i>
                  </span>
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Nhập tên tác giả"
                    value={editAuthorName}
                    onChange={(e) => seteditAuthorName(e.target.value)}
                  />
                </div>
              </Col>
              <Col>
                <button className="btn btn-primary" type="submit">
                  Update
                </button>
              </Col>
            </Row>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={handleClose}>
              Close
            </Button>
            <Button variant="primary" onClick={handleUpdate}>
              Save Changes
            </Button>
          </Modal.Footer>
        </Modal>
      </Fragment>
    </>
  );
};

export default CreateAuthorName;
