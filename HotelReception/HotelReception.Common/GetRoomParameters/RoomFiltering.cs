using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Common.GetRoomParameters
{
    public class RoomFiltering
    {
        public Guid? Id { get; set; }
        public Nullable<int> Number { get; set; }
        public Guid? RoomTypeID { get; set; }
        public Nullable<int> RoomFloor { get; set; }
        //
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Floor { get; set; }
        public string Description { get; set; }
        public Nullable<int> HasBalcony { get; set; }
        public Nullable<int> NumberOfBeds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
