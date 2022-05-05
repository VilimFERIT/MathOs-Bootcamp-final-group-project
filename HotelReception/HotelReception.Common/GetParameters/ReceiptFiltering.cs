using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Common.GetParameters
{
    public class ReceiptFiltering
    {
        public Nullable<int> Id { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<int> ReservationId { get; set; }
    }
}
