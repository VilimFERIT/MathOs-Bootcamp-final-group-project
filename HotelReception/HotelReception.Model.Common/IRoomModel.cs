using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model.Common
{
    public interface IRoomModel
    {
        Guid Id { get; set; }
        int Number { get; set; }
        Guid RoomTypeID { get; set; }
        int RoomFloor { get; set; }
        decimal Price { get; set; }
    }
}
