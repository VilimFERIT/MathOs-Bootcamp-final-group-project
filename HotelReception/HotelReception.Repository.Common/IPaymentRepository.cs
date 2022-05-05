using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;

namespace HotelReception.Repository.Common
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetPaymentsAsync(Sorting sorting, Paging paging, PaymentFiltering paymentFiltering);
    }
}
