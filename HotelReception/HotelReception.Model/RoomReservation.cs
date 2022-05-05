using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Model.Common;

namespace HotelReception.Model
{
    public class RoomReservation : IRoomReservation
    {
        public Guid RoomId { get; set; }
        public Guid ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
