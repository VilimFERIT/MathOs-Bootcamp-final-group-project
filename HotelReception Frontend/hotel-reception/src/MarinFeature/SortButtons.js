import { useEffect, useState } from "react";
import "./style.css";

export default function SortButtons({ setOrder, setBy, name }) {
  const [buttonClicked, setButtonClicked] = useState();

  useEffect(() => {
    let elements = document.getElementsByClassName("sortBtn");
    for (let i = 0; i < elements.length; i++) {
      if (elements[i].id != buttonClicked) {
        elements[i].style.color = "black";
      } else {
        elements[i].style.color = "lightblue";
      }
    }
  }, [buttonClicked]);

  const doSort = (event) => {
    let button = document.getElementById(
      `${event.target.name}${event.target.value}`
    ).id;
    setButtonClicked(button);
    console.log(buttonClicked);
    setOrder(event.target.value);
    setBy(event.target.name);
  };

  return (
    <div>
      <button
        id={`${name}asc`}
        name={name}
        className="sortBtn"
        value="asc"
        onClick={doSort}
      >
        ↑
      </button>

      <button
        id={`${name}desc`}
        name={name}
        className="sortBtn"
        value="desc"
        onClick={doSort}
      >
        ↓
      </button>
    </div>
  );
}
