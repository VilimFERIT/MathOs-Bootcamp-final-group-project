using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelReception.WebApi.Models
{
    public class RESTRoom
    {

        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid RoomTypeID { get; set; }
        public int RoomFloor { get; set; }

        public RESTRoom(Guid id, int number, Guid roomType, int roomFloor)
        {
            Id = id;
            Number = Number;
            RoomTypeID = roomType;
            RoomFloor = roomFloor;

        }

        public RESTRoom()
        {
        }

    }
}