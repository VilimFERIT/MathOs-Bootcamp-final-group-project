using HotelReception.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model
{
    public class ActiveGuestModel : IActiveGuestModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Pid { get; set; }

        public int RoomNumber { get; set; }

        public bool IsActive { get; set; }

        public string StreetName { get; set; }

        public string CityName { get; set; }

        public string CountryName { get; set; }

        public bool select { get; set; }

    }
}
