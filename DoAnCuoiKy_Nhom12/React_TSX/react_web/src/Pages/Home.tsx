import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";

function Home() {
  return (
    <>
      <div id="carouselExample" className="carousel slide">
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
      </div>
    </>
  );
}

export default Home;
