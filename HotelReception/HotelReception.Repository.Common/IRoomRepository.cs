using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Common.GetRoomParameters;
using HotelReception.Model;
using HotelReception.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomFiltering = HotelReception.Common.GetRoomParameters.RoomFiltering;

namespace HotelReception.Repository.Common
{
    public interface IRoomRepository
    {
        //Task<List<IRoomModel>> GetRoomsAsync(RoomReservationSorting sorting, Paging paging, RoomReservationFiltering roomReservationFiltering);

        Task<List<IRoomModel>> GetRoomByIdAsync(Guid roomId);
        //Task<List<AvailableRoom>> GetRoomsAsync(RoomReservationSorting sorting, Paging paging, RoomReservationFiltering roomReservationFiltering);
        Task<List<AvailableRoom>> GetRoomsAsync(RoomSorting sorting, Paging paging, RoomFiltering roomReservationFiltering);
    }
}
