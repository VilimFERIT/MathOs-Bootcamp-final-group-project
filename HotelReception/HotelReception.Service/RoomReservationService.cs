using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository.Common;
using HotelReception.Service.Common;

namespace HotelReception.Service
{
    public class RoomReservationService : IRoomReservationService
    {
        protected IRoomReservationRepository RoomReservationRepository { get; set; }

        public RoomReservationService(IRoomReservationRepository roomReservationRepository)
        {
            RoomReservationRepository = roomReservationRepository;
        }

        public async Task<List<IRoomReservation>> GetRoomReservationsAsync(RoomReservationSorting sorting, Paging paging, RoomReservationFiltering roomReservationFiltering)
        {
            return await RoomReservationRepository.GetRoomsAsync(sorting, paging, roomReservationFiltering);
        }

        
        public async Task PostRoomReservationAsync(RoomReservation roomReservation)
        {
            await RoomReservationRepository.PostRoomReservationAsync(roomReservation);
        }
    }
}
