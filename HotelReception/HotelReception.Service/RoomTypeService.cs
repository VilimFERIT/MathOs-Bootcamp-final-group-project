using HotelReception.Model;
using HotelReception.Repository;
using HotelReception.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Service
{
    public class RoomTypeService : IRoomTypeService
    {

        List<RoomTypeModel> roomType = new List<RoomTypeModel>();

        public async Task<List<RoomTypeModel>> GetAll()
        {
            RoomTypeRepository roomTypeRepository = new RoomTypeRepository();
            return await roomTypeRepository.GetAll();
        }

        public async Task<RoomTypeModel> GetRoomTypeById(Guid id)
        {
            RoomTypeRepository roomTypeRepository = new RoomTypeRepository();
            return await roomTypeRepository.GetRoomTypeById(id);
        }


        public async Task PostColumnAsync(RoomTypeModel roomType)
        {
            RoomTypeRepository roomTypeRepository = new RoomTypeRepository();
            await roomTypeRepository.PostColumnAsync(roomType);
        }

        public async Task NewRoomType(Guid id, RoomTypeModel roomType)
        {
            RoomTypeRepository roomTypeRepository = new RoomTypeRepository();
            await roomTypeRepository.NewRoomType(id, roomType);
        }
        public async Task DeleteRoomTypeById(Guid Id)
        {
            RoomTypeRepository roomTypeRepository = new RoomTypeRepository();
            await roomTypeRepository.DeleteRoomTypeById(Id);
        }
    }
}
