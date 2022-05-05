using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model.Common
{
    public interface IActiveGuestModel
    {
        Guid Id { get; set; }
        string FirstName { get; set; }

       string LastName { get; set; }

        string Pid { get; set; }

         int RoomNumber { get; set; }

        bool IsActive { get; set; }

        string StreetName { get; set; }

        string CityName { get; set; }

        string CountryName { get; set; }

        bool select { get; set; }
    }
}
