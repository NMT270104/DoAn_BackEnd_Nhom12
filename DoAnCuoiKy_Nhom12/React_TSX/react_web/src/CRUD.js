import React, { useState, useEffect, Fragment } from "react";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import axios from "axios";
import { ToastContainer, toast } from "react-toastify"; 
import 'react-toastify/dist/ReactToastify.css'

const defaultImageSrc="/images/Doraemon1.jpg"

const CRUD = () => {
  const [show, setShow] = useState(false);
  const [authors, setAuthors] = useState([]);
  const [categories, setCategories] = useState([]);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [NameBook, setNameBook] = useState("");
  const [AuthorName, setAuthorName] = useState("");
  const [CategoryName, setCategoryName] = useState("");
  const [Price, setPrice] = useState("");
  const [Quantity, setQuantity] = useState("");
  const [Description, setDescription] = useState("");
  const [Image, setImage] = useState(null);

  const [editBook, setEditBook] = useState({
    BookID: "",
    AuthorID: "",
    CategoryID: "",
    NameBook: "",
    AuthorName: "",
    CategoryName: "",
    Price: "",
    Quantity: "",
    Description: "",
    ImageFile:null,
    ImageSrc:"",
    Image: "",
  });

  const [data, setData] = useState([]);

  useEffect(() => {
    fetchData();
    getCategories();
  }, []);

  const fetchData = async () => {
    try {
      const [authorsResponse, dataResponse] = await axios.all([
        axios.get("http://localhost:40080/api/Authors/GetAuthors"),
        axios.get(
          "http://localhost:40080/api/Books/Get?pageIndex=0&pageSize=10&sortColumn=NameBook&sortOrder=ASC"
        ),
      ]);
      setAuthors(authorsResponse.data);
      setData(dataResponse.data.Data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const getCategories = () => {
    axios
      .get("http://localhost:40080/api/Categories/GetCategories")
      .then((result) => {
        setCategories(result.data);
      })
      .catch((error) => {
        console.error("Error fetching categories:", error);
      });
  };

  const handleEdit = (book) => {
    // Set các state cho Modal
    setNameBook(book.NameBook);
    setAuthorName(book.AuthorName);
    setCategoryName(book.CategoryName);
    setPrice(book.Price);
    setQuantity(book.Quantity);
    setDescription(book.Description);
    setImage(null);

    // Set EditBook state
    setEditBook({
      BookID: book.BookID,
      AuthorID: book.AuthorID,
      CategoryID: book.CategoryID,
      NameBook: book.NameBook,
      AuthorName: book.AuthorName,
      CategoryName: book.CategoryName,
      Price: book.Price,
      Quantity: book.Quantity,
      Description: book.Description,
      Image: null,
    });
    handleShow();
  };

  const handleDelete = (bookID) => {
    if (window.confirm("Bạn có muốn xóa cuốn sách này không?")) {
      alert(bookID);
    }
  };

  const handleUpdate = () => {
    handleClose();
    fetchData();
  };

  // Function để thêm mới sản phẩm
  const handleSave = async () => {
    const booksUrl =
      "http://localhost:40080/api/Books/Get?pageIndex=0&pageSize=10&sortColumn=NameBook&sortOrder=ASC";
    const authorsUrl = "http://localhost:40080/api/Authors/CreateAuthor";
    const categoriesUrl =
      "http://localhost:40080/api/Categories/CreateCategory";

    try {
      // Dữ liệu tác giả mới từ state
      const newAuthorData = { AuthorName };
      const authorResponse = await axios.post(authorsUrl, newAuthorData);
      const AuthorID = authorResponse.data.AuthorID;

      // Dữ liệu thể loại mới từ state
      const newCategoryData = { CategoryName };
      const categoryResponse = await axios.post(categoriesUrl, newCategoryData);
      const CategoryID = categoryResponse.data.CategoryID;

      // Dữ liệu sách mới từ state
      const newBookData = {
        NameBook,
        AuthorID,
        CategoryID,
        Price,
        Quantity,
        Description,
        Image, // Chắc chắn rằng Image là một đối tượng File nếu bạn đang sử dụng multipart/form-data
      };

      // Gửi yêu cầu POST để thêm sách mới
      await axios.post(booksUrl, newBookData);

      fetchData(); // Load lại danh sách sau khi thêm mới
      clear(); // Xóa trạng thái của form
      toast.success("Sách đã được thêm mới");
    } catch (error) {
      console.error("Error adding new book:", error);
      toast.error("Đã xảy ra lỗi khi thêm mới sách");
    }
  };

  const clear = () => {
    setNameBook("");
    setAuthorName("");
    setCategoryName("");
    setPrice("");
    setQuantity("");
    setDescription("");
    setImage(null);
  };

  const getAuthorName = (authorID) => {
    const author = authors.find((a) => a.AuthorID === authorID);
    return author ? author.AuthorName : "Unknown Author";
  };

  const getCategoryName = (categoryID) => {
    const category = categories.find((c) => c.CategoryID === categoryID);
    return category ? category.CategoryName : "Unknown Category";
  };

  const showPreview = (e) => {
    if (e.target.files && e.target.files[0]) {
      let imageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = (x) => {
        setImage({
          ...Image,
          imageFile: imageFile,
          ImageSrc: x.target.result,
        });
      };
      reader.readAsDataURL(imageFile);
    } else {
      setImage({
        ...Image,
        imageFile: null,
        ImageSrc: defaultImageSrc,
      });
    }
  };

  return (
    <Fragment>
      <ToastContainer></ToastContainer>
      <Container className="mt-5">
        <Row className="justify-content-center">
          <Col className="col-lg-6">
            <div className="input-group mb-3">
              <span className="input-group-text">
                <i className="fa-solid fa-book"></i>
              </span>
              <input
                type="text"
                className="form-control"
                placeholder="Nhập tên sách"
                value={NameBook}
                onChange={(e) => setNameBook(e.target.value)}
              />
            </div>
            <div className="input-group mb-3">
              <span className="input-group-text">
                <i className="fa-solid fa-user" />
              </span>
              <input
                type="text"
                className="form-control"
                placeholder="Nhập tên tác giả"
                value={AuthorName}
                onChange={(e) => setAuthorName(e.target.value)}
              />
            </div>
            <div className="input-group mb-3">
              <select
                className="form-select"
                value={CategoryName}
                onChange={(e) => setCategoryName(e.target.value)}
              >
                <option selected value="">
                  Chọn thể loại
                </option>
                {categories.map((category) => (
                  <option
                    key={category.CategoryID}
                    value={category.CategoryName}
                  >
                    {category.CategoryName}
                  </option>
                ))}
              </select>
            </div>
            <div className="input-group mb-3">
              <span className="input-group-text">
                <i className="fa-solid fa-money-bill" />
              </span>
              <input
                type="text"
                className="form-control"
                placeholder="Nhập giá tiền"
                value={Price}
                onChange={(e) => setPrice(e.target.value)}
              />
            </div>
            <div className="input-group mb-3">
              <span className="input-group-text">
                <i className="fa-solid fa-box" />
              </span>
              <input
                type="text"
                className="form-control"
                placeholder="Nhập số lượng"
                value={Quantity}
                onChange={(e) => setQuantity(e.target.value)}
              />
            </div>
            <div className="form-floating mb-3">
              <textarea
                className="form-control"
                placeholder="Leave a comment here"
                id="floatingTextarea"
                value={Description}
                onChange={(e) => setDescription(e.target.value)}
              ></textarea>
              <label htmlFor="floatingTextarea">Nhập mô tả</label>
            </div>
            <div className="mb-3">
              <label htmlFor="formFile" className="form-label">
                Nhập hình ảnh
              </label>
              <input
                className="form-control"
                type="file"
                id="formFile"
                accept="image/*"
                onChange={showPreview}
              />
            </div>
            <div className="d-flex justify-content-center align-items-center">
              <button
                className="btn btn-primary"
                type="button"
                onClick={() => handleSave()}
              >
                Submit
              </button>
            </div>
          </Col>
        </Row>
      </Container>

      <br></br>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>#</th>
            <th>BookID</th>
            <th>AuthorID</th>
            <th>CategoryID</th>
            <th>Tên sách</th>
            <th>Tên tác giả</th>
            <th>Thể loại</th>
            <th>Giá tiền</th>
            <th>Số lượng</th>
            <th>Mô tả</th>
            <th>Hình ảnh</th>
          </tr>
        </thead>
        <tbody>
          {data && data.length > 0
            ? data.map((item, index) => {
                return (
                  <tr key={index}>
                    <td>{index + 1}</td>
                    <td>{item.BookID}</td>
                    <td>{item.AuthorID}</td>
                    <td>{item.CategoryID}</td>
                    <td>{item.NameBook}</td>
                    <td>{getAuthorName(item.AuthorID)}</td>
                    <td>{getCategoryName(item.CategoryID)}</td>
                    <td>{item.Price}</td>
                    <td>{item.Quantity}</td>
                    <td>{item.Description}</td>
                    <td>
                      <img
                        src={item.ImageSrc} // hoặc sử dụng đường dẫn hình ảnh
                        alt="Hình ảnh sách"
                        style={{ width: "50px", height: "50px" }}
                      />
                    </td>
                    <td colSpan={2}>
                      <button
                        className="btn btn-primary"
                        onClick={() => handleEdit(item)}
                      >
                        Edit
                      </button>{" "}
                      &nbsp;
                      <button
                        className="btn btn-danger"
                        onClick={() => handleDelete(item.BookID)}
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
          <Modal.Title>Chỉnh sửa / Cập nhật</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Container className="mt-3">
            <Row className="justify-content-center">
              <Col>
                <div className="input-group mb-3">
                  <span className="input-group-text">
                    <i className="fa-solid fa-book"></i>
                  </span>
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Nhập tên sách"
                    value={editBook.NameBook}
                    onChange={(e) =>
                      setEditBook({ ...editBook, NameBook: e.target.value })
                    }
                  />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">
                    <i className="fa-solid fa-user" />
                  </span>
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Nhập tên tác giả"
                    value={editBook.AuthorName}
                    onChange={(e) =>
                      setEditBook({ ...editBook, AuthorName: e.target.value })
                    }
                  />
                </div>
                <div className="input-group mb-3">
                  <select
                    className="form-select"
                    value={editBook.CategoryName}
                    onChange={(e) =>
                      setEditBook({ ...editBook, CategoryName: e.target.value })
                    }
                  >
                    <option selected value="">
                      Chọn thể loại
                    </option>
                    {categories.map((category) => (
                      <option
                        key={category.CategoryID}
                        value={category.CategoryName}
                      >
                        {category.CategoryName}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">
                    <i className="fa-solid fa-money-bill" />
                  </span>
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Nhập giá tiền"
                    value={editBook.Price}
                    onChange={(e) =>
                      setEditBook({ ...editBook, Price: e.target.value })
                    }
                  />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">
                    <i className="fa-solid fa-box" />
                  </span>
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Nhập số lượng"
                    value={editBook.Quantity}
                    onChange={(e) =>
                      setEditBook({ ...editBook, Quantity: e.target.value })
                    }
                  />
                </div>
                <div className="form-floating mb-3">
                  <textarea
                    className="form-control"
                    placeholder="Leave a comment here"
                    id="floatingTextarea"
                    value={editBook.Description}
                    onChange={(e) =>
                      setEditBook({ ...editBook, Description: e.target.value })
                    }
                  ></textarea>
                  <label htmlFor="floatingTextarea">Nhập mô tả</label>
                </div>
                <div className="mb-3">
                  <label htmlFor="formFile" className="form-label">
                    Nhập hình ảnh
                  </label>
                  <input
                    className="form-control"
                    type="file"
                    id="formFile"
                    onChange={(e) =>
                      setEditBook({ ...editBook, Image: e.target.files[0] })
                    }
                  />
                </div>
                <div className="d-flex justify-content-center align-items-center">
                  <button
                    className="btn btn-primary"
                    type="button"
                    onClick={() => handleUpdate()}
                  >
                    Submit
                  </button>
                </div>
              </Col>
            </Row>
          </Container>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={() => handleUpdate()}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
    </Fragment>
  );
};

export default CRUD;
