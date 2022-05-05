using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;

namespace HotelReception.Service.Common
{
    public interface IReservationService
    {
        Task<List<IReservation>> GetReservationsAsync(Sorting sorting, Paging paging, ReservationFiltering filtering);

        Task PostReservationAsync(InsertReservationDomain reservationReceipt);

        Task PutReservationAsync(Guid id);
    }
}
