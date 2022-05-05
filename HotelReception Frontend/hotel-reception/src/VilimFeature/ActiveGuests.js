import axios from "axios";
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Button, Form, Table, input, Label } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import "./VilimFeature.css";

function ActiveGuests() {
  const [guests, getGuests] = useState([]);
  const [submitCheck, setSubmitCheck] = useState(false);

  const clickFunction = () => {
    axios.put("https://localhost:44307/guest/activitycheck", guests);
    //fetchGuests();
    window.location.reload(false);

    // if (submitCheck == false) {
    //   setSubmitCheck(true);
    // } else {
    //   setSubmitCheck(false);
  };

  useEffect(() => {
    fetchGuests();
    //changeGuestActivity();
  }, []);

  /*const [checkedGuest, getSelectedGuests] = useState([]);

  const changeGuestActivity = async () => {
    let urlString = "https://localhost:44307/guest/getactiveguests?";
    const changedGuests = guests.map((guest) => {
      return {
        FirstName: guest.FirstName,
        LastName: guest.LastName,
        Pid: guest.Pid,
        RoomNumber: guest.RoomNumber,
        check: guest.select,
      };
    });
    if (changedGuests) {
      urlString = urlString + `Pid=${changedGuests.Pid}`;
      fetch(urlString);
    }
  };*/

  const onSubmit = (e) => {
    e.preventDefault();
  };

  const fetchGuests = async () => {
    let sortByChecked = document.querySelector(
      'input[name="sortByForm"]:checked'
    );

    let sortOrderChecked = document.querySelector(
      'input[name="sortOrderForm"]:checked'
    );
    let firstNameFilter =
      document.forms["firstNameForm"]["firstNameinput"].value;
    let lastNameFilter = document.forms["lastNameForm"]["lastNameinput"].value;
    let pidFilter = document.forms["pidFilterForm"]["pidinput"].value;
    let roomNumberFilter =
      document.forms["roomNumberForm"]["roomNumberinput"].value;

    let urlString = "https://localhost:44307/guest/getactiveguests";
    if (sortByChecked != null) {
      urlString = urlString + "?SortBy=" + sortByChecked.value;
    }
    if (sortOrderChecked != null) {
      urlString =
        urlString +
        "&SortOrder=" +
        sortOrderChecked.value +
        "&pagesize=50" +
        "&IsActive=true";
    }

    /*if (sortByChecked == null && sortOrderChecked == null) {
      urlString =
        "https://localhost:44307/guest/getactiveguests?sortby=lastname&sortorder=desc&pagenumber=1&pagesize=4&IsActive=true";
    }*/

    if (firstNameFilter != null) {
      urlString = urlString + `&FirstName=${firstNameFilter}`;
    }

    if (lastNameFilter != null) {
      urlString = urlString + `&LastName=${lastNameFilter}`;
    }

    if (pidFilter != null) {
      urlString = urlString + `&Pid=${pidFilter}`;
    }

    if (roomNumberFilter != null) {
      urlString = urlString + `&RoomNumber=${roomNumberFilter}`;
    }

    const data = await fetch(urlString);
    const guests = await data.json();

    const mappedGuests = guests.map((guest) => {
      return {
        Id: guest.Id,
        FirstName: guest.FirstName,
        LastName: guest.LastName,
        Pid: guest.Pid,
        RoomNumber: guest.RoomNumber,
        IsActive: guest.IsActive,
        StreetName: guest.StreetName,
        CityName: guest.CityName,
        CountryName: guest.CountryName,
        select: false,
      };
    });
    /*if (mappedGuests.select == true) {
      fetch(`https://localhost:44307/guest/activity?Pid=${mappedGuests.Pid}`);
    }*/
    getGuests(mappedGuests);
  };
  var listOfGuests = guests.map((guest) => (
    <tr>
      <td>{guest.FirstName}</td>
      <td>{guest.LastName}</td>
      <td>{guest.Pid}</td>
      <td>{guest.RoomNumber}</td>
      <td>{guest.StreetName}</td>
      <td>{guest.CityName}</td>
      <td>{guest.CountryName}</td>
      <td>
        <input
          type="checkbox"
          name={`checkout${guest.Id}`}
          onChange={(event) => {
            let checked = event.target.checked;

            getGuests(
              guests.map((data) => {
                if (guest.Pid === data.Pid) {
                  data.select = checked;
                }
                return data;
              })
            );
          }}
          checked={guest.select}
        ></input>
      </td>
    </tr>
  ));
  //checkbox change
  return (
    <div id="VilimDiv">
      <div id="sortDiv">
        <Form name="sortByForm">
          <span>Sort by: </span>
          <input
            className="input"
            type="radio"
            name="sortByForm"
            id="sortFirstName"
            value="FirstName"
          ></input>
          <Label htmlFor="filterFirstName">First Name</Label>

          <input
            className="input"
            type="radio"
            name="sortByForm"
            id="sortLastName"
            value="LastName"
            defaultChecked
          ></input>
          <Label htmlFor="filterLastName">Last Name</Label>

          <input
            className="input"
            type="radio"
            name="sortByForm"
            id="sortPid"
            value="Pid"
          ></input>
          <Label htmlFor="filterProjectId">PID</Label>

          <input
            className="input"
            type="radio"
            name="sortByForm"
            id="sortRoomNumber"
            value="Number"
          ></input>
          <Label htmlFor="filterProjectId">Room number</Label>
        </Form>

        <Form name="sortOrderForm">
          <span>Sort order: </span>
          <input
            className="input"
            type="radio"
            name="sortOrderForm"
            id="sortAsc"
            value="Asc"
          ></input>
          <Label htmlFor="sortAsc">ASC</Label>

          <input
            className="input"
            type="radio"
            name="sortOrderForm"
            id="sortDesc"
            value="Desc"
            defaultChecked
          ></input>
          <Label htmlFor="sortDesc">DESC</Label>
        </Form>
      </div>

      <Button id="sortBtn" onClick={fetchGuests}>
        Sort
      </Button>

      <Form name="firstNameForm" onSubmit={onSubmit}>
        <input id="firstNameinput" type="text" placeholder="First name"></input>
      </Form>

      <Form name="lastNameForm" onSubmit={onSubmit}>
        <input id="lastNameinput" type="text" placeholder="Last name"></input>
      </Form>

      <Form name="pidFilterForm" onSubmit={onSubmit}>
        <input id="pidinput" type="text" placeholder="PID"></input>
      </Form>

      <Form name="roomNumberForm" onSubmit={onSubmit}>
        <input
          id="roomNumberinput"
          type="text"
          placeholder="Room number"
        ></input>
      </Form>

      <Button id="filterButton" onClick={fetchGuests}>
        Filter
      </Button>

      <Table>
        <thead>
          <tr>
            <th>First name</th>
            <th>Last name</th>
            <th>PID</th>
            <th>Room number</th>
            <th>Address</th>
            <th>City</th>
            <th>Country</th>
            <th>Checkout</th>
          </tr>
        </thead>

        <tbody>{listOfGuests}</tbody>
      </Table>

      <Button type="button" onClick={clickFunction}>
        Checkout
      </Button>
    </div>
  );
}

export default ActiveGuests;
