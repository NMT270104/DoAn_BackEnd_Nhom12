import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";

function Home() {
  return (
    <>
      {/* <div id="carouselExample" className="carousel slide">
        <div className="carousel-inner">
          <div className="carousel-item active c-item">
            <img src="/images/1.jpg" className="d-block w-100 c-img" alt="..." />
          </div>
          <div className="carousel-item c-item">
            <img src="/images/book2copy.jpg" className="d-block w-100 c-img" alt="..." />
          </div>
          <div className="carousel-item c-item">
            <img src="/images/2.webp" className="d-block w-100 c-img" alt="..." />
          </div>
        </div>
        <button
          className="carousel-control-prev"
          type="button"
          data-bs-target="#carouselExample"
          data-bs-slide="prev"
        >
          <span className="carousel-control-prev-icon" aria-hidden="true" />
          <span className="visually-hidden">Previous</span>
        </button>
        <button
          className="carousel-control-next"
          type="button"
          data-bs-target="#carouselExample"
          data-bs-slide="next"
        >
          <span className="carousel-control-next-icon" aria-hidden="true" />
          <span className="visually-hidden">Next</span>
        </button>
      </div> */}

      <div>
        <header className="mt-2">
          <div className="container">
            <div className="row">
              <div className="col-3 col-xl-3 logo">
                <h2>E-BOOK</h2>
              </div>
              <div className=" col-9 col-md-8 col-lg-8 col-xl-5">
                <div className="input-group mb-3">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Tìm kiếm ở đây"
                    aria-label="Recipient's username"
                    aria-describedby="button-addon2"
                  />
                  <button
                    className="btn btn-outline-secondary"
                    type="button"
                    id="button-addon2"
                  >
                    Tìm kiếm
                  </button>
                </div>
              </div>
              <div className="col-1 col-md-2 col-lg-8 col-xl-2 mx-md-auto text-md-center mx-xl-auto text-xl-center">
                <a href="/#">
                  <i className="fa-solid fa-user fs-4 icon-logo text-muted" />
                </a>
              </div>
              <div className="col-1 col-md-2 col-lg-8 col-xl-2 mx-md-auto text-md-center mx-xl-auto text-xl-center">
                <a href="/#">
                  <i className="fa-solid fa-cart-shopping fs-4 icon-logo text-muted" />
                </a>
              </div>
            </div>
          </div>
        </header>
        {/* body */}
        <img src="/images/bg1.jpg" alt="..." className="img-fluid" />
        <div className="bg-color d-flex justify-content-center align-items-center">
          <img
            src="/images/bg2.gif"
            alt="..."
            className="img-fluid mx-auto my-auto"
          />
        </div>
        {/* card start */}
        <section className="sec">
          <div className="container">
            <div className="row row-cols-sm-1 row-cols-md-2 row-cols-lg-4">
              <div className="col-sm-12 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
                <div className="products">
                  <div className="card">
                    <div className="img img-fluid">
                      <img src="images/Doraemon1.jpg" alt="..." />
                    </div>
                    <div className="title text-break text-center">
                      Doraemon tap 1
                    </div>
                    <div className="box">
                      <div className="price text-success fs-4">50$</div>
                    </div>
                    <div className="btn-buy">
                      <button className="btn btn-danger">Buy Now</button>
                    </div>
                  </div>
                </div>
              </div>
              <div className="col-sm-12 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
                <div className="products">
                  <div className="card">
                    <div className="img img-fluid">
                      <img src="images/Doraemon1.jpg" alt="..." />
                    </div>
                    <div className="title text-break text-center">
                      Doraemon tap 1
                    </div>
                    <div className="box">
                      <div className="price text-success fs-4">50$</div>
                    </div>
                    <div className="btn-buy">
                      <button className="btn btn-danger">Buy Now</button>
                    </div>
                  </div>
                </div>
              </div>
              <div className="col-sm-12 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
                <div className="products">
                  <div className="card">
                    <div className="img img-fluid">
                      <img src="images/Doraemon1.jpg" alt="..." />
                    </div>
                    <div className="title text-break text-center">
                      Doraemon tap 1
                    </div>
                    <div className="box">
                      <div className="price text-success fs-4">50$</div>
                    </div>
                    <div className="btn-buy">
                      <button className="btn btn-danger">Buy Now</button>
                    </div>
                  </div>
                </div>
              </div>
              <div className="col-sm-12 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
                <div className="products">
                  <div className="card">
                    <div className="img img-fluid">
                      <img src="images/Doraemon1.jpg" alt="..." />
                    </div>
                    <div className="title text-break text-center">
                      Doraemon tap 1
                    </div>
                    <div className="box">
                      <div className="price text-success fs-4">50$</div>
                    </div>
                    <div className="btn-buy">
                      <button className="btn btn-danger">Buy Now</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
        <footer>
          <div className="container">
            <div className="row row-cols-lg-5 row-cols-md-2">
              <div className="col-sm-12 col-md-12 text-center">
                <h1>E-Book</h1>
                <p>
                  Find and explore the best eBooks from all your favorite
                  writers
                </p>
                <hr />
              </div>
              <div className="col-sm-12 col-md-6 text-center">
                <h1>About</h1>
                <p>Awards</p>
                <p>FAQs</p>
                <p>Privacy policy</p>
                <p>Terms of services</p>
                <hr />
              </div>
              <div className="col-sm-12 col-md-6 text-center">
                <h1>Company</h1>
                <p>Blogs</p>
                <p>Community</p>
                <p>Our team</p>
                <p>Help center</p>
                <hr />
              </div>
              <div className="col-sm-12 col-md-6 text-center">
                <h1>Contact</h1>
                <p>Nguyễn Minh Thư</p>
                <p>Đinh Quốc Hưng</p>
                <p>Nguyễn Châu Trường Giang</p>
                <p>ebook@gmail.com</p>
                <hr />
              </div>
              <div className="col-sm-12 col-md-6 text-center">
                <h1>Social</h1>
                <p>
                  <i className="fa-brands fa-facebook" /> Facebook
                </p>
                <p>
                  <i className="fa-brands fa-instagram" /> Instagram
                </p>
                <p>
                  <i className="fa-solid fa-x" /> Twitter
                </p>
                <hr />
              </div>
            </div>
          </div>
        </footer>
      </div>
    </>
  );
}

export default Home;
