using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model
{
    public class AvailableRoom
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int RoomFloor { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool HasBalcony { get; set; }
        public int NumberOfBeds { get; set; }

    }
}
