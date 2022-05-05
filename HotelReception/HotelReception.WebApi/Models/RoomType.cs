using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelReception.WebApi.Models
{
    public class RESTRoomType
    {

        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool HasBalcony { get; set; }
        public int NumberOfBeds { get; set; }

        public RESTRoomType(Guid id, string description, bool hasBalcony, int numberOfBens)
        {
            Id = id;
            Description = description;
            HasBalcony = hasBalcony;
            NumberOfBeds = numberOfBens;

        }

        public RESTRoomType()
        {
        }

    }
}