using HotelReception.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model
{
    public class GuestModel : IGuestModel
    {
        public Guid Id { get; set; }
        public string Pid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid RoomId { get; set; }
        public Guid AddressId { get; set; }

        public IAddressModel Address { get; set; }

        public bool IsActive { get; set; }
    }
}
