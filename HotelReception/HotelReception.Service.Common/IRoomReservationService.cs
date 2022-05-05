using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;

namespace HotelReception.Service.Common
{
    public interface IRoomReservationService
    {
        Task<List<IRoomReservation>> GetRoomReservationsAsync(RoomReservationSorting sorting, Paging paging, RoomReservationFiltering roomReservationFiltering);

        Task PostRoomReservationAsync(RoomReservation roomReservation);
    }
}
