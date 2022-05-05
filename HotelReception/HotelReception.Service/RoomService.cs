using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Common.GetRoomParameters;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository;
using HotelReception.Repository.Common;
using HotelReception.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Service
{
    public class RoomService : IRoomService
    {
        protected IRoomRepository RoomRepository { get; set; }

        public RoomService(IRoomRepository roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public async Task<List<AvailableRoom>> GetRoomsAsync(RoomSorting sorting, Paging paging, RoomFiltering roomFiltering)
        {
            return await RoomRepository.GetRoomsAsync(sorting, paging, roomFiltering);


            //RoomSorting roomSorting, RoomPaging roomPaging, RoomFiltering roomFiltering
            //return RoomRepository.GetRooms(roomSorting, roomPaging, roomFiltering);
        }

        public async Task<List<IRoomModel>> GetRoomById(Guid roomId)
        {
            return await RoomRepository.GetRoomByIdAsync(roomId);
        }
    }
    //public class RoomService : IRoomService
    //{

    //    List<RoomModel> room = new List<RoomModel>();

    //    public async Task<List<RoomModel>> GetAllRooms()
    //    {
    //        RoomRepository roomRepository = new RoomRepository();
    //        return await roomRepository.GetAllRooms();
    //    }

    //    public async Task<List<RoomModel>> GetRoomByFloor(int roomFloor)
    //    {
    //        RoomRepository roomRepository = new RoomRepository();
    //        return await roomRepository.GetRoomByFloor(roomFloor);
    //    }

    //    public async Task<List<RoomModel>> GetRoomByNumber(int number)
    //    {
    //        RoomRepository roomRepository = new RoomRepository();
    //        return await roomRepository.GetRoomByNumber(number);
    //    }

    //    public async Task<List<RoomModel>> GetRoomById(Guid id)
    //    {
    //        RoomRepository roomRepository = new RoomRepository();
    //        return await roomRepository.GetRoomById(id);
    //    }

    //    public async Task<List<RoomModel>> GetByRoomTypeID(Guid roomTypeID)
    //    {
    //        RoomRepository roomRepository = new RoomRepository();
    //        return await roomRepository.GetByRoomTypeID(roomTypeID);
    //    }



    //public async Task<RoomModel> GetRoomById(string id)
    //{
    //    RoomRepository roomRepository = new RoomRepository();
    //    return await roomRepository.GetRoomById(id);
    //}





    //public async Task PostColumnAsync(RoomModel room)
    //{
    //    RoomRepository roomRepository = new RoomRepository();
    //    await roomRepository.PostColumnAsync(room);
    //}

    //public async Task NewRoom(int id, RoomModel room)
    //{
    //    RoomRepository roomRepository = new RoomRepository();
    //    await roomRepository.NewRoom(id, room);
    //}
    //public async Task DeleteRoomById(int Id)
    //{
    //    RoomRepository roomRepository = new RoomRepository();
    //    await roomRepository.DeleteRoomById(Id);
    //}
}

