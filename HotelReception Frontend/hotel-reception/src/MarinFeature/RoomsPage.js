import { useParams } from "react-router-dom";
import axios from "axios";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Table, Input, Label } from "reactstrap";
import Paging from "./Paging";
import SortButtons from "./SortButtons";
import Filtering from "./Filtering";
import "./style.css";
import App from "../App";

export function RoomsPage() {
  const linkStyle = { textDecoration: "none" };
  const { from, to, pageNumber, pageSize, sortOrder, sortBy } = useParams();

  const [sortB, setSortB] = useState(sortBy);
  const [sortOrd, setSortOrder] = useState(sortOrder);
  const [pageNum, setPageNum] = useState(pageNumber);
  const [pageS, setPageS] = useState(pageSize);
  const [rooms, setRooms] = useState([]);
  const [totalRooms, setTotalRooms] = useState(0);
  const [filterList, setFilterList] = useState({
    number: "",
    price: "",
    floor: "",
    description: "",
    hasBalcony: "",
    numberOfBeds: "",
  });
  const [filterCheck, setFilterCheck] = useState(false);

  useEffect(() => {
    let totalPages = totalRooms / pageS;
    let currentPage = document.getElementById(`page${pageNum}`);

    for (let i = 0; i < Math.ceil(totalPages + 1); i++) {
      if (document.getElementById(`page${i}`) != null) {
        if (document.getElementById(`page${i}`) != currentPage) {
          document.getElementById(`page${i}`).style.backgroundColor = "white";
        } else {
          document.getElementById(`page${i}`).style.backgroundColor =
            "lightblue";
        }
      }
    }
  });

  useEffect(() => {
    let urlStringTotal = `https://localhost:44307/api/GetRooms?startDate=${from}&endDate=${to}`;
    axios.get(urlStringTotal).then((response) => {
      setTotalRooms(response.data.length);
    });

    let filterString = "";
    for (const [key, value] of Object.entries(filterList)) {
      if (value != "") {
        filterString += `&${key}=${value}`;
      }
    }

    let urlString = `https://localhost:44307/api/GetRooms?startDate=${from}&endDate=${to}&pageNumber=${pageNum}&pageSize=${pageS}&sortOrder=${sortOrd}&sortBy=${sortB}`;
    if (filterString != null) {
      urlString += filterString;
    }

    axios.get(urlString).then((response) => {
      console.log(urlString);
      setRooms(response.data);
    });
  }, [pageNum, pageS, sortOrd, filterCheck]);

  return (
    <div id="roomsDiv">
      <Label for="pageSizeInput">Results per page:</Label>
      <Input
        value={pageS}
        style={{ width: "25%" }}
        id="pageSizeInput"
        onChange={(event) => {
          setPageS(event.target.value);
          setPageNum(1);
          console.log(pageS);
          console.log(pageNum);
        }}
        type="select"
      >
        <option value={3} onClick={(event) => setPageS(event.target.value)}>
          3
        </option>

        <option value="6" onClick={(event) => setPageS(event.target.value)}>
          6
        </option>
      </Input>
      <Table hover>
        <thead>
          <tr>
            <th className="tableHead">
              Room number
              <SortButtons
                name="number"
                setOrder={setSortOrder}
                setBy={setSortB}
              />
            </th>

            <th className="tableHead">
              Price
              <SortButtons
                name="price"
                setOrder={setSortOrder}
                setBy={setSortB}
              />
            </th>
            <th className="tableHead">
              Floor
              <SortButtons
                name="roomFloor"
                setOrder={setSortOrder}
                setBy={setSortB}
              />
            </th>
            <th className="tableHead">
              Description
              <SortButtons
                name="description"
                setOrder={setSortOrder}
                setBy={setSortB}
              />
            </th>
            <th className="tableHead">
              HasBalcony
              <SortButtons
                name="hasBalcony"
                setOrder={setSortOrder}
                setBy={setSortB}
              />
            </th>
            <th className="tableHead">
              NumberOfBeds
              <SortButtons
                name="numberOfBeds"
                setOrder={setSortOrder}
                setBy={setSortB}
              />
            </th>
          </tr>
        </thead>

        <tbody>
          {rooms != null
            ? rooms
                .sort((a, b) =>
                  sortOrd == "asc" ? a.Number - b.Number : b.Number - a.Number
                )
                .map((room) => (
                  <tr key={room.Id}>
                    <td className="roomData">{room.Number}</td>
                    <td>
                      {`$`}
                      {room.Price}
                    </td>
                    <td className="roomData">{room.RoomFloor}</td>
                    <td className="roomData">{room.Description}</td>
                    <td className="roomData">
                      {room.HasBalcony == false ? "no" : "yes"}
                    </td>
                    <td className="roomData">{room.NumberOfBeds}</td>
                    <td>
                      <button>
                        <Link
                          to={`/newguest/startDate=${from}-endDate=${to}-roomNumber=${room.Number}`}
                          state={{ rooms }}
                          style={linkStyle}
                        >
                          Make a reservation
                        </Link>
                      </button>
                    </td>
                  </tr>
                ))
            : null}
        </tbody>
      </Table>

      <Paging
        pageSize={pageS}
        pageNumber={pageNum}
        setPageNumber={setPageNum}
        numberOfRooms={totalRooms}
        from={from}
        to={to}
        sortOrder={sortOrd}
        sortBy={sortB}
      />
      <Filtering
        setFilter={setFilterList}
        filterList={filterList}
        setFilterCheck={setFilterCheck}
        filterCheck={filterCheck}
      />
    </div>
  );
}
