using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Model.Common;

namespace HotelReception.Model
{
    public class RoomModel : IRoomModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid RoomTypeID { get; set; }

        public bool IsFree { get; set; }
        public int RoomFloor { get; set; }
        public Decimal Price { get; set; }

    }
}
