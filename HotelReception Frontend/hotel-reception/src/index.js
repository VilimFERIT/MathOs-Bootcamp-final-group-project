import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import {
  Navigation,
  Footer,
  NewReservation,
  ActiveGuests,
  InactiveGuests,
  ReservationList,
} from "./andrej-components";
import App from "./App";
import Login from "./Franjo_componenets/LoginForm";
import { AuthProvider } from "./Franjo_componenets/AuthProvider";

ReactDOM.render(
  //<AuthProvider>
  <Login />,
  //</AuthProvider>,

  document.getElementById("root")
);

/*
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <AuthProvider>
      <App />
    </AuthProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
*/
