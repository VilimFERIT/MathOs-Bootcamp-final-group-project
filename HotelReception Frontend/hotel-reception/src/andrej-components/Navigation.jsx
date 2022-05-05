import React from "react";
import { NavLink } from "react-router-dom";
import { Button } from "reactstrap";

function removeItem() {
  localStorage.removeItem("Auth");
  window.location.reload(true);
}

function Navigation() {
  return (
    <div className="navigation">
      <nav className="navbar navbar-expand navbar-dark bg-dark">
        <div className="container">
          <div>
            <ul className="navbar-nav ml-auto">
              <li className="nav-item">
                <NavLink className="nav-link" to="/">
                  New reservation
                  <span className="sr-only">(current)</span>
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/activeguests">
                  Active guests
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/guestarchive">
                  Inactive guests
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/reservationlist">
                  Reservation list
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link" to="/login">
                  <Button onClick={removeItem}>Log out</Button>
                </NavLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </div>
  );
}

export default Navigation;
