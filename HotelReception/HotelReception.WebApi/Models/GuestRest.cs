using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelReception.WebApi.Models
{
    public class GuestRest
    {
        public Guid Id { get; set; }

        public string Pid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsPayer { get; set; }

        public Guid RoomId { get; set; }

        public Guid AddressId { get; set; }

        public bool IsActive { get; set; }
    }
}