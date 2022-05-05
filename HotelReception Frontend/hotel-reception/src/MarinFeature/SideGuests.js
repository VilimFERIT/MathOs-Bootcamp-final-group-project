import axios from "axios";
import { useState, useEffect } from "react";
import { Button, Form, FormGroup, Input, Label, Row, Col } from "reactstrap";
import "./style.css";

export default function SideGuests(props) {
  const [countries, setCountries] = useState([]);
  const [otherGuest, setOtherGuest] = useState({
    firstName: "",
    lastName: "",
    pid: "",
    name: "", // Country name
    cityName: "",
    postalOfficeNumber: "",
    streetName: "",
    roomNumber: props.room,
  });

  console.log(otherGuest);

  var listOfCountries = countries.map((country) => (
    <option key={country.Id} value={country.Name}>
      {country.Name}
    </option>
  ));

  useEffect(() => {
    let urlStringCountry =
      "https://localhost:44307/api/getcountries?sortBy=id&sortOrder=asc";
    axios.get(urlStringCountry).then((response) => {
      setCountries(response.data);
    });
  }, []);

  const handleChangeGuests = (event) => {
    const value = event.target.value;
    setOtherGuest({ ...otherGuest, [event.target.name]: value });

    if (event.target.value == "") {
      event.target.style.borderColor = "red";
    } else {
      event.target.style = {
        borderColor: "white",
        boxShadow: "none",
      };
    }
  };

  const submitOtherGuest = (event) => {
    event.preventDefault();

    // Check if all input fields are filled in
    let nullCheck = false;
    Object.keys(otherGuest).forEach(function (key) {
      if (otherGuest[key] === "") {
        nullCheck = true;
        document.getElementById(
          `${key}${props.list.length}`
        ).style.borderColor = "red";
      }
    });

    if (nullCheck === false) {
      const newGuest = {
        firstName: otherGuest.firstName,
        lastName: otherGuest.lastName,
        pid: otherGuest.pid,
        name: otherGuest.name, // Country name
        cityName: otherGuest.cityName,
        postalOfficeNumber: otherGuest.postalOfficeNumber,
        streetName: otherGuest.streetName,
        roomNumber: otherGuest.roomNumber,
      };

      let urlStringGuest = "https://localhost:44307/guest/addnew";

      axios.post(urlStringGuest, newGuest).then((response) => {
        console.log(response.data);
        document.getElementById("otherGuestSubmitBtn").disabled = "true";
        const elements = document.getElementsByClassName("sideGuestsInput");
        for (let i = 0; i < elements.length; i++) {
          elements[i].disabled = "true";
        }
        document.getElementById("otherSubmitMessage").textContent =
          "Successfully submitted!";
      });
    }
  };

  return (
    <Form>
      <h1>Guest {props.list.length + 2}</h1>
      <Row>
        <Col md={6}>
          <FormGroup>
            <Label for="firstName">First name: </Label>
            <Input
              className="sideGuestsInput"
              type="text"
              name="firstName"
              id={`firstName${props.list.length}`}
              onChange={handleChangeGuests}
            />
          </FormGroup>
        </Col>

        <Col md={6}>
          <FormGroup>
            <Label for="lastName">Last name: </Label>
            <Input
              className="sideGuestsInput"
              type="text"
              name="lastName"
              id={`lastName${props.list.length}`}
              onChange={handleChangeGuests}
            />
          </FormGroup>
        </Col>
      </Row>

      <FormGroup>
        <Label for="pid">PID: </Label>
        <Input
          className="sideGuestsInput"
          type="text"
          name="pid"
          id={`pid${props.list.length}`}
          onChange={handleChangeGuests}
        />
      </FormGroup>

      <FormGroup>
        <Label For="name">Country: </Label>
        <Input
          className="sideGuestsInput"
          type="select"
          name="name"
          id={`name${props.list.length}`}
          onChange={handleChangeGuests}
        >
          <option hidden>Select an option</option>
          {listOfCountries}
        </Input>
      </FormGroup>

      <Row>
        <Col md={6}>
          <FormGroup>
            <Label for="cityName">City: </Label>
            <Input
              className="sideGuestsInput"
              type="text"
              name="cityName"
              id={`cityName${props.list.length}`}
              onChange={handleChangeGuests}
            />
          </FormGroup>
        </Col>

        <Col>
          <FormGroup>
            <Label for="postalOfficeNumber">Postal office number: </Label>
            <Input
              className="sideGuestsInput"
              type="text"
              name="postalOfficeNumber"
              id={`postalOfficeNumber${props.list.length}`}
              onChange={handleChangeGuests}
            />
          </FormGroup>
        </Col>

        <Col>
          <FormGroup>
            <Label for="streetName">Street name: </Label>
            <Input
              className="sideGuestsInput"
              type="text"
              name="streetName"
              id={`streetName${props.list.length}`}
              onChange={handleChangeGuests}
            />
          </FormGroup>
        </Col>
      </Row>

      <FormGroup>
        <Label for="roomNumber">Room number: </Label>
        <Input
          className="sideGuestsInput"
          type="text"
          name="roomNumber"
          id={`roomNumber${props.list.length}`}
          value={props.room}
          placeholder={props.room}
          disabled
        />
      </FormGroup>

      <Button onClick={submitOtherGuest} id="otherGuestSubmitBtn">
        Submit
      </Button>
      <span className="submitClass" id="otherSubmitMessage"></span>
    </Form>
  );
}
