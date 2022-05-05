import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import ActiveGuests from "./VilimFeature/ActiveGuests";
import InactiveGuests from "./VilimFeature/InactiveGuests";
import DateForm from "./MarinFeature/DateForm";
import GuestInfoForm from "./MarinFeature/GuestInfoForm";
import { RoomsPage } from "./MarinFeature/RoomsPage";
import { Navigation } from "./andrej-components";
import { ReservationList } from "./andrej-components";

export default function App() {
  return (
    <div>
      <Router>
        <Navigation />
        <Routes>
          <Route path="/" element={<DateForm />} />
          <Route path="/activeguests" element={<ActiveGuests />} />
          <Route path="/guestarchive" element={<InactiveGuests />} />

          <Route path="/newguest/roomNumber=:number" />
          <Route
            path="/rooms/startDate=:from-endDate=:to-pageSize=:pageSize-pageNumber=:pageNumber-sortOrder=:sortOrder-sortBy=:sortBy"
            element={<RoomsPage />}
          />
          <Route
            path="/newguest/startDate=:from-endDate=:to-roomNumber=:roomNum"
            element={<GuestInfoForm />}
          />
          <Route path="/reservationlist" element={<ReservationList />} />
        </Routes>
      </Router>
    </div>
  );
}
