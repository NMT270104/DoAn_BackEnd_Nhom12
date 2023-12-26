import React from "react";

function Product() {
  return (
    <>
      <h2 className="text-center">Products</h2>
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
    </>
  );
}

export default Product;
