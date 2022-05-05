using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model
{
    public class RoomTypeModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool HasBalcony { get; set; }
        public int NumberOfBeds { get; set; }

        public RoomTypeModel(Guid id, string description, bool hasBalcony, int numberOfBens)
        {
            Id = id;
            Description = description;
            HasBalcony = hasBalcony;
            NumberOfBeds = numberOfBens;

        }

        public RoomTypeModel()
        {
        }
    }
}
