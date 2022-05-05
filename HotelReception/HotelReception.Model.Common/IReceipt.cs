using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model.Common
{
    public interface IReceipt
    {
        Guid Id { get; set; }
        decimal Price { get; set; }
        Guid PaymentMethod { get; set; }
        Guid ReservationId { get; set; }
    }
}
