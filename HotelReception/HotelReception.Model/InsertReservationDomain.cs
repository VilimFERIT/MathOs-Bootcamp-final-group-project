using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model
{
    public class InsertReservationDomain
    {

        public Guid RecId { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethod { get; set; }
        public Guid ReservationId { get; set; }
        //---------------


        public Guid ResId { get; set; }
        public Guid ReceptionistId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        //--------------


        public string FirstName { get; set; }
        public string LastName { get; set; }
        //---------------


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //----------------


        public string Pid { get; set; }
    }
}
