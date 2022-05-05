import React from "react";
import AndrejFilter from "./AndrejFilter";

function ReservationList() {
  return (
    <div className="reservation">
      <div class="container">
        <div class="row align-items-center my-5">
          <div class="col-lg-10">
            <img
              class="img-fluid rounded mb-4 mb-lg-0"
              src="http://placehold.it/900x400"
              alt=""
            />
          </div>
          <div class="col-lg-5">
            <AndrejFilter />
          </div>
        </div>
      </div>
    </div>
  );
}

export default ReservationList;
