using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Repository.Common;
using HotelReception.Service.Common;

namespace HotelReception.Service
{
    public class PaymentService : IPaymentService
    {
        protected IPaymentRepository PaymentRepository { get; set; }

        public PaymentService(IPaymentRepository repository)
        {
            PaymentRepository = repository;
        }


        public async Task<List<Payment>> GetPaymentsAsync(Sorting sorting, Paging paging, PaymentFiltering paymentFiltering)
        {
            return await PaymentRepository.GetPaymentsAsync(sorting, paging, paymentFiltering);
        }
    }
}
