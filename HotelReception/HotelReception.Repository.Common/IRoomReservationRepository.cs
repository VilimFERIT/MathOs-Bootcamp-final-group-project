using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;

namespace HotelReception.Repository.Common
{
    public interface IRoomReservationRepository
    {
        Task<List<IRoomReservation>> GetRoomsAsync(RoomReservationSorting sorting, Paging paging, RoomReservationFiltering roomReservationFiltering);

        Task PostRoomReservationAsync(RoomReservation roomReservation);
    }
}
