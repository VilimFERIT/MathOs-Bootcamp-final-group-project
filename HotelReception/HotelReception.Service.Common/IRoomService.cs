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

namespace HotelReception.Service.Common
{
    public interface IRoomService
    {
        //Task<List<IRoomModel>> GetRoomsAsync(RoomReservationSorting sorting, Paging paging, RoomReservationFiltering roomReservationFiltering);

        Task<List<IRoomModel>> GetRoomById(Guid roomId);
        //Task<List<AvailableRoom>> GetRoomsAsync(RoomReservationSorting sorting, Paging paging, RoomReservationFiltering roomReservationFiltering);
        Task<List<AvailableRoom>> GetRoomsAsync(RoomSorting sorting, Paging paging, RoomFiltering roomReservationFiltering);
    }
}




