using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Common.GetParameters
{
    public class GuestFiltering
    {
        public Guid? Id { get; set; }
        public string Pid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Nullable<bool> IsPayer { get; set; }

        public Nullable<Guid> RoomId { get; set; }

        public Nullable<Guid> AddressId { get; set; }

        public Nullable<bool> IsActive { get; set; }
    }
}
