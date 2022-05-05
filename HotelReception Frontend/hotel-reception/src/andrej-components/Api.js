import "./App.css";
import React, { Component } from "react";
import axios from "axios";
import { Table, Button } from "reactstrap";

class App extends Component {
  state = {
    spheres: [],
  };
  componentWillMount() {
    axios
      .get("http://localhost:44307/api/GetReservations" + activestatus)
      .then((response) => {
        this.setState({
          spheres: response.data,
        });
      });
  }

  render() {
    let spheres = this.state.spheres.map((globe) => {
      return (
        <tr>
          <td>{globe.Id}</td>
          <td>{globe.CreationDate}</td>
          <td>{globe.IsActive}</td>
        </tr>
      );
    });

    return (
      <div className="App container">
        <Table>
          <thead>
            <tr>
              <th>
                <Button id="action" onClick={this.handleClick}>
                  Active
                </Button>
                <Button id="action" onClick={this.handleClick}>
                  Inactive
                </Button>
              </th>
              <th>Planet Name:</th>
              <th>Planet Location:</th>
            </tr>
          </thead>

          <tbody>{spheres}</tbody>
        </Table>
      </div>
    );
  }
}

export default App;
