import { Button } from "bootstrap";
import { Input, Label } from "reactstrap";
import "./style.css";

export default function Filtering({
  setFilter,
  filterList,
  setFilterCheck,
  filterCheck,
}) {
  const handleChange = (event) => {
    setFilter({ ...filterList, [event.target.name]: event.target.value });
  };

  return (
    <div>
      <Label>Room number: </Label>
      <Input
        name="number"
        onChange={handleChange}
        className="filterInput"
        type="text"
      />

      <Label>Price: </Label>
      <Input
        name="price"
        onChange={handleChange}
        className="filterInput"
        type="text"
      ></Input>

      <Label>Floor: </Label>
      <Input
        name="floor"
        onChange={handleChange}
        className="filterInput"
        type="text"
      ></Input>

      <Label>Description: </Label>
      <Input
        name="description"
        onChange={handleChange}
        className="filterInput"
        type="text"
      ></Input>

      <Label>HasBalcony: </Label>
      <Input
        name="hasBalcony"
        onChange={handleChange}
        className="filterInput"
        type="text"
      ></Input>

      <Label>NumberOfBeds: </Label>
      <Input
        name="numberOfBeds"
        onChange={handleChange}
        className="filterInput"
        type="text"
      ></Input>

      <button
        onClick={() => {
          if (filterCheck == true) {
            setFilterCheck(false);
          } else {
            setFilterCheck(true);
          }
        }}
      >
        Submit
      </button>
    </div>
  );
}
