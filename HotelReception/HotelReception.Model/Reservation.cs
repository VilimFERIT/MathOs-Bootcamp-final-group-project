using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Model.Common;

namespace HotelReception.Model
{
    public class Reservation : IReservation
    {
        public Guid Id { get; set; }
        public Guid ReceptionistId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public Guid GuestId { get; set; }
        public IGuestModel guest { get; set; }
    }
}
