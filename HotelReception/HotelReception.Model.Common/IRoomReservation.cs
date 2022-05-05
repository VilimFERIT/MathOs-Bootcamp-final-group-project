using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model.Common
{
    public interface IRoomReservation
    {
        Guid RoomId { get; set; }
        Guid ReservationId { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
