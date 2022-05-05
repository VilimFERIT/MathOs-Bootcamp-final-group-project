import axios from "axios";
import { useState, useEffect } from "react";
import { useLocation, useParams } from "react-router-dom";
import { Button, Form, FormGroup, Input, Label, Row, Col } from "reactstrap";
import "./style.css";
import SideGuests from "./SideGuests";

export default function GuestInfoForm() {
  const { roomNum } = useParams();
  const { from } = useParams();
  const { to } = useParams();
  const location = useLocation();
  const { rooms } = location.state;

  const [max, setMax] = useState();
  const [counter, setCounter] = useState(1);
  const [formList, setFormList] = useState([]);
  const [countries, setCountries] = useState([]);
  const [payments, setPayments] = useState([]);
  const [guestInfo, setGuestInfo] = useState({
    // Guest info
    firstName: "",
    lastName: "",
    pid: "",
    name: "", // Country name
    cityName: "",
    postalOfficeNumber: "",
    streetName: "",
    roomNumber: roomNum,
    // Reservation info
    paymentMethod: "",
    startDate: from,
    endDate: to,
    recFirstName: "",
    recLastName: "",
  });

  useEffect(() => {
    for (let i = 0; i < rooms.length; i++) {
      if (rooms[i].Number == roomNum) {
        var maxGuests = rooms[i].NumberOfBeds;
        setMax(maxGuests);
      }
    }
  }, []);

  var listOfPayments = payments.map((payment) => (
    <option key={payment.Id} value={payment.paymentMethod}>
      {payment.Method}
    </option>
  ));
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

  useEffect(() => {
    let urlStringPayment = "https://localhost:44307/api/getPayments";
    axios.get(urlStringPayment).then((response) => {
      setPayments(response.data);
    });
  }, []);

  const handleChange = (event) => {
    const value = event.target.value;
    setGuestInfo({ ...guestInfo, [event.target.name]: value });

    // Check if the guest is already in the database (set as inactive)
    if (event.target.name == "pid") {
      let urlString = `https://localhost:44307/guest/getall?pid=${event.target.value}`;
      axios.get(urlString).then((response) => {
        if (response.data.length == 1) {
          document.getElementById("pidBtn").style.display = "block";
          const elements = document.getElementsByClassName("payerInput");
          console.log(elements);
          for (let i = 0; i < elements.length; i++) {
            elements[i].disabled = "true";
          }
        } else {
          document.getElementById("pidBtn").style.display = "none";
          const elements = document.getElementsByClassName("payerInput");
          for (let i = 0; i < elements.length; i++) {
            elements[i].disabled = "";
          }
        }
      });
    }

    if (event.target.value == "") {
      event.target.style.borderColor = "red";
    } else {
      event.target.style = {
        borderColor: "white",
        boxShadow: "none",
      };
    }
  };

  const submitGuest = (event) => {
    event.preventDefault();

    // Check if all input fields are filled in
    let nullCheck = false;
    Object.keys(guestInfo).forEach(function (key) {
      if (guestInfo[key] === "") {
        nullCheck = true;
        document.getElementById(`${key}`).style.borderColor = "red";
      }
    });

    if (nullCheck === false) {
      const newGuest = {
        firstName: guestInfo.firstName,
        lastName: guestInfo.lastName,
        pid: guestInfo.pid,
        name: guestInfo.name, // Country name
        cityName: guestInfo.cityName,
        postalOfficeNumber: guestInfo.postalOfficeNumber,
        streetName: guestInfo.streetName,
        roomNumber: guestInfo.roomNumber,
      };

      const newReservation = {
        paymentMethod: guestInfo.paymentMethod,
        pid: guestInfo.pid,
        startDate: guestInfo.startDate,
        endDate: guestInfo.endDate,
        firstName: guestInfo.recFirstName, // Receptionist's first and last name
        lastName: guestInfo.recLastName,
      };

      let urlStringGuest = "https://localhost:44307/guest/addnew";
      let urlStringReservation = "https://localhost:44307/api/PostReservation";

      axios.post(urlStringGuest, newGuest).then((response) => {
        if (response.data != null) {
          axios.post(urlStringReservation, newReservation).then((response) => {
            document.getElementById("guestSubmitBtn").disabled = "true";
            const elements = document.getElementsByClassName("payerInput");
            const elements2 =
              document.getElementsByClassName("reservationInput");
            for (let i = 0; i < elements.length; i++) {
              elements[i].disabled = "true";
            }
            for (let i = 0; i < elements2.length; i++) {
              elements2[i].disabled = "true";
            }
            document.getElementById("submitMessage").textContent =
              "Successfully submitted!";
          });
        }
      });
    }
  };

  const addNewForm = (event) => {
    if (counter + 1 == max) {
      document.getElementById("addNewGuestBtn").disabled = "true";
      setFormList(
        formList.concat(<SideGuests list={formList} room={`${roomNum}`} />)
      );
    } else {
      let tempValue = counter + 1;
      setCounter(tempValue);
      setFormList(
        formList.concat(<SideGuests list={formList} room={`${roomNum}`} />)
      );
    }
  };

  return (
    <div id="guestDiv">
      <Form name="guestForm">
        <h1>Payer</h1>
        <Row>
          <Col md={6}>
            <FormGroup>
              <Label for="firstName">First name: </Label>
              <Input
                className="payerInput"
                type="text"
                name="firstName"
                id="firstName"
                value={guestInfo.firstName}
                onChange={handleChange}
              />
            </FormGroup>
          </Col>

          <Col md={6}>
            <FormGroup>
              <Label for="lastName">Last name: </Label>
              <Input
                className="payerInput"
                type="text"
                name="lastName"
                id="lastName"
                value={guestInfo.lastName}
                onChange={handleChange}
              />
            </FormGroup>
          </Col>
        </Row>

        <FormGroup>
          <Label for="pid">PID: </Label>
          <Input
            className="reservationInput" // Not payerInput because it would be disabled
            type="text"
            name="pid"
            id="pid"
            value={guestInfo.pid}
            onChange={handleChange}
          />
          <button id="pidBtn">Update</button>
        </FormGroup>

        <FormGroup>
          <Label htmlFor="name">Country: </Label>
          <Input
            className="payerInput"
            type="select"
            name="name"
            id="name"
            value={guestInfo.name}
            onChange={handleChange}
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
                className="payerInput"
                type="text"
                name="cityName"
                id="cityName"
                value={guestInfo.cityName}
                onChange={handleChange}
              />
            </FormGroup>
          </Col>

          <Col>
            <FormGroup>
              <Label for="postalOfficeNumber">Postal office number: </Label>
              <Input
                className="payerInput"
                type="text"
                name="postalOfficeNumber"
                id="postalOfficeNumber"
                value={guestInfo.postalOfficeNumber}
                onChange={handleChange}
              />
            </FormGroup>
          </Col>

          <Col>
            <FormGroup>
              <Label for="streetName">Street name: </Label>
              <Input
                className="payerInput"
                type="text"
                name="streetName"
                id="streetName"
                value={guestInfo.streetName}
                onChange={handleChange}
              />
            </FormGroup>
          </Col>
        </Row>

        <FormGroup>
          <Label for="roomNumber">Room number: </Label>
          <Input
            disabled
            type="text"
            name="roomNumber"
            id="roomNumber"
            value={roomNum}
            placeholder={roomNum}
          />
        </FormGroup>

        <FormGroup>
          <Label for="paymentMethod">Payment method: </Label>
          <Input
            className="reservationInput"
            type="select"
            name="paymentMethod"
            id="paymentMethod"
            value={guestInfo.paymentMethod}
            onChange={handleChange}
          >
            <option hidden>Select an option</option>
            {listOfPayments}
          </Input>
        </FormGroup>

        <Row>
          <Col>
            <FormGroup>
              <Label for="recFirstName">Receptionist's first name: </Label>
              <Input
                className="reservationInput"
                type="text"
                name="recFirstName"
                id="recFirstName"
                value={guestInfo.recFirstName}
                onChange={handleChange}
              />
            </FormGroup>
          </Col>

          <Col>
            <FormGroup>
              <Label for="recLastName">Receptionist's last name: </Label>
              <Input
                className="reservationInput"
                type="text"
                name="recLastName"
                id="recLastName"
                value={guestInfo.recLastName}
                onChange={handleChange}
              />
            </FormGroup>
          </Col>
        </Row>

        <Button onClick={submitGuest} id="guestSubmitBtn">
          Submit
        </Button>
        <span className="submitClass" id="submitMessage"></span>
      </Form>
      <br></br>
      <br></br>
      <Button onClick={addNewForm} id="addNewGuestBtn">
        Add new guest
      </Button>
      {formList}
    </div>
  );
}
