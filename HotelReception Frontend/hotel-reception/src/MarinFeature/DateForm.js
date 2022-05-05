import { useState } from "react";
import { Link } from "react-router-dom";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";

export default function DateForm() {
  const [fromInput, setFromInput] = useState("");
  const [toInput, setToInput] = useState("");
  const [pageSize, setPageSize] = useState(3);
  const [pageNumber, setPageNumber] = useState(1);
  const [sortOrder, setSortOrder] = useState("asc");
  const [sortBy, setSortBy] = useState("number");
  console.log(fromInput);
  console.log(toInput);

  const handleChangeFrom = () => {
    setFromInput(document.getElementById("roomsFrom").value);
  };
  const handleChangeTo = () => {
    setToInput(document.getElementById("roomsTo").value);
  };

  return (
    <div id="dateDiv">
      <Form name="dateInputForm">
        <FormGroup>
          <Label for="roomsFrom">From: </Label>
          <Input onChange={handleChangeFrom} type="date" id="roomsFrom" />
        </FormGroup>
        <br></br>
        <br></br>

        <FormGroup>
          <Label for="roomsTo">To: </Label>
          <Input onChange={handleChangeTo} type="date" id="roomsTo" />
        </FormGroup>
        <br></br>
        <br></br>

        <Link
          to={`/rooms/startDate=${fromInput}-endDate=${toInput}-pageSize=${pageSize}-pageNumber=${pageNumber}-sortOrder=${sortOrder}-sortBy=${sortBy}`}
        >
          <Button id="submitBtn">Find available rooms</Button>
        </Link>
      </Form>
    </div>
  );
}
