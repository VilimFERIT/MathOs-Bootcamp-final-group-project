using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Common.GetParameters
{
    public class ActiveGuestFiltering
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Pid { get; set; }

        public Nullable<int> RoomNumber { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public string StreetName { get; set; }

        public string CityName { get; set; }

        public string CountryName { get; set; }
    }
}
