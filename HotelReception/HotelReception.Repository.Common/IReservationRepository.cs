using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;

namespace HotelReception.Repository.Common
{
    public interface IReservationRepository
    {
        Task<List<IReservation>> GetReservationsAsync(Sorting sorting, Paging paging, ReservationFiltering filtering);

        Task PostReservationAsync(Receipt receipt, Reservation reservation, ReceptionistModel receptionist, RoomReservation roomReservation, Payment payment, string pid);

        Task PutReservationAsync(Guid id);
    }
}
