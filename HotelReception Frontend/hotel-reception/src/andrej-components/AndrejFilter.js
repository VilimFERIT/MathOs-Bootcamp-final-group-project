import React, { Component } from "react";
import axios from "axios";
import { Table, Button } from "reactstrap";

class AndrejFilter extends Component {
  state = {
    reservations: [],
    activestatus: "",
  };
  componentDidUpdate() {
    axios
      .get(
        "https://localhost:44307/api/GetReservations/" +
          `${this.state.activestatus}`
      )
      .then((response) => {
        this.setState({
          reservations: response.data,
        });
      });
  }

  showAll = (e) => {
    e.preventDefault();
    console.log(e.target.value);
    this.setState({ activestatus: e.target.value });
  };

  showActive = (e) => {
    e.preventDefault();
    console.log(e.target.value);
    this.setState({ activestatus: e.target.value });
  };

  showInactive = (e) => {
    e.preventDefault();
    console.log(e.target.value);
    this.setState({ activestatus: e.target.value });
  };

  render() {
    /*let reservations = this.state.reservations.map((reservation) => {
      return (
        <tr>
          <td>{reservation.CreationDate}</td>
          <td>{reservation.guest.FirstName}</td>
          <td>{reservation.guest.LastName}</td>
        </tr>
      );
    }*/

    return (
      <div className="App container">
        <Button
          class="activity-button"
          value={""}
          id="active"
          onClick={this.showAll}
        >
          All
        </Button>
        <Button
          class="activity-button"
          value={"?IsActive=1"}
          id="active"
          onClick={this.showActive}
        >
          Active
        </Button>
        <Button
          class="activity-button"
          value={"?IsActive=0"}
          id="inactive"
          onClick={this.showInactive}
        >
          Inactive
        </Button>
        <Table>
          <thead>
            <tr>
              <th>Creation Date:</th>
              <th>FirstName:</th>
              <th>LastName:</th>
            </tr>
          </thead>

          <tbody>
            {this.state.reservations != null
              ? this.state.reservations.map((reservation) => {
                  return (
                    <tr>
                      <td>{reservation.CreationDate}</td>
                      <td>{reservation.guest.FirstName}</td>
                      <td>{reservation.guest.LastName}</td>
                    </tr>
                  );
                })
              : null}
          </tbody>
        </Table>
      </div>
    );
  }
}

export default AndrejFilter;
