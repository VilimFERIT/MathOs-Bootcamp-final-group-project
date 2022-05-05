using HotelReception.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Common.GetReservation
{
    public class ReservationFiltering
    {
        public Guid? Id { get; set; }
        public Guid? ReceptionistId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? IsActive { get; set; }
        public GuestModel guest { get; set; }

    }
}

