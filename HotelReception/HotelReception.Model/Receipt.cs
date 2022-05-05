using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Model.Common;

namespace HotelReception.Model
{
    public class Receipt : IReceipt
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public Guid PaymentMethod { get; set; }
        public Guid ReservationId { get; set; }
    }
}
