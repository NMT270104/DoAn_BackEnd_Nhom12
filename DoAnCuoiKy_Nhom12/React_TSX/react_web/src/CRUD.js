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
    Image: null,
  });

  const [data, setData] = useState([]);

  const fetchAuthors = async () => {
    try {
      // Gửi yêu cầu để đăng nhập và nhận token
      const loginResponse = await axios.post(
        "http://localhost:40080/Account/Login",
        {
          UserName: "Admin",
          PassWord: "Admin@123",
        }
      );

      // Lấy token từ phản hồi đăng nhập
      const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbmlzdHJhdG9yIiwiZXhwIjoxNzAzNzY0MDIwLCJpc3MiOiJXZWJBUEkiLCJhdWQiOiJXZWJBUEkifQ.54-VEt9r3PVakR3lJnKXmKLGEewoT3HNBn8FmgyOiqw";

      // Sử dụng token để gửi yêu cầu lấy dữ liệu từ API có quyền admin
      const authorsResponse = await axios.get(
        "http://localhost:40080/api/Admin/GetAuthors",
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      // Xử lý dữ liệu tại đây (ví dụ: cập nhật state authors)
      setAuthors(authorsResponse.data);
    } catch (error) {
      console.error("Error fetching authors:", error);
    }
  };


  useEffect(() => {
    // Gọi hàm để lấy dữ liệu tác giả khi component được tạo
    fetchAuthors();
  }, []);


  const handleEdit = (book) => {
    setNameBook(book.NameBook);
    setAuthorName(getAuthorName(book.AuthorID));
    setCategoryName(getCategoryName(book.CategoryID));
    setPrice(book.Price);
    setQuantity(book.Quantity);
    setDescription(book.Description);
    setImage(null);

    setEditBook({
      BookID: book.BookID,
      AuthorID: book.AuthorID,
      CategoryID: book.CategoryID,
      NameBook: book.NameBook,
      AuthorName: getAuthorName(book.AuthorID),
      CategoryName: getCategoryName(book.CategoryID),
      Price: book.Price,
      Quantity: book.Quantity,
      Description: book.Description,
      Image: null,
    });

    handleShow();
  };

  const handleDelete = async (bookID) => {
    if (window.confirm("Bạn có muốn xóa cuốn sách này không?")) {
      try {
        // Gửi yêu cầu DELETE để xóa sách
        await axios.delete(`http://localhost:40080/api/Admin/DeleteBook/${bookID}`);
        toast.success("Sách đã được xóa");

      } catch (error) {
        console.error("Error deleting book:", error);
        toast.error("Đã xảy ra lỗi khi xóa sách");
      }
    }
  };

  const handleUpdate = async () => {
    try {
      // Gửi yêu cầu PUT để cập nhật thông tin sách
      const formData = new FormData();
      formData.append("BookID", editBook.BookID);
      formData.append("NameBook", editBook.NameBook);
      formData.append("AuthorID", editBook.AuthorID);
      formData.append("CategoryID", editBook.CategoryID);
      formData.append("Price", editBook.Price);
      formData.append("Quantity", editBook.Quantity);
      formData.append("Description", editBook.Description);
      formData.append("Image", editBook.Image);

      await axios.put("http://localhost:40080/api/Admin/UpdateBook", formData);
      toast.success("Thông tin sách đã được cập nhật");
      handleClose();
    } catch (error) {
      console.error("Error updating book:", error);
      toast.error("Đã xảy ra lỗi khi cập nhật sách");
    }
  };

  const handleSave = async () => {
    try {
      const author = authors.find((a) => a.AuthorName === AuthorName);
      const category = categories.find((c) => c.CategoryName === CategoryName);

      // Kiểm tra xem tác giả đã tồn tại hay chưa, nếu chưa thì tạo mới
      if (!author) {
        const newAuthorResponse = await axios.post(
          "http://localhost:40080/api/Admin/CreateAuthor",
          { AuthorName }
        );
        setAuthors([...authors, newAuthorResponse.data]);
      }

      // Kiểm tra xem thể loại đã tồn tại hay chưa, nếu chưa thì tạo mới
      if (!category) {
        const newCategoryResponse = await axios.post(
          "http://localhost:40080/api/Admin/CreateCategory",
          { CategoryName }
        );
        setCategories([...categories, newCategoryResponse.data]);
      }

      // Lấy ID của tác giả và thể loại
      const AuthorID = author ? author.AuthorID : authors[authors.length - 1].AuthorID + 1;
      const CategoryID = category ? category.CategoryID : categories[categories.length - 1].CategoryID + 1;

      // Tạo mới sách
      await axios.post("http://localhost:40080/api/Admin/CreateBook", {
        NameBook,
        AuthorID,
        CategoryID,
        Price,
        Quantity,
        Description,
        Image, // Chắc chắn rằng Image là một đối tượng File nếu bạn đang sử dụng multipart/form-data
      });

      
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
                onChange={""}
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
                    <td>{item.bookID}</td>
                    <td>{item.AuthorID}</td>
                    <td>{item.CategoryID}</td>
                    <td>{item.NameBook}</td>
                    <td>{getAuthorName(item.AuthorID)}</td>
                    <td>{getCategoryName(item.CategoryID)}</td>
                    <td>{item.Price}</td>
                    <td>{item.Quantity}</td>
                    <td>{item.Description}</td>
                    <td>
                      {item.Image && (
                        <img
                          src={item.Image} // Đảm bảo rằng item.Image là một đường dẫn đến hình ảnh
                          alt="Hình ảnh sách"
                          style={{ width: "50px", height: "50px" }}
                        />
                      )}
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
