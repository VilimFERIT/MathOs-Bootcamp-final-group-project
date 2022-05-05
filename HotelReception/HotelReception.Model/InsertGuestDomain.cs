using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model
{
    public class InsertGuestDomain
    {
        // Properties for guest -------------------------
        public Guid GuestIdPrimary { get; set; }
        public string Pid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid RoomId { get; set; }
        public Guid AddressId { get; set; }

        //public IAddressModel Address { get; set; }

        public bool IsActive { get; set; }


        // Properties for country ----------------
        public Guid CountryIdPrimary { get; set; }

        public string Name { get; set; }


        // Properties for address ------------------
        public Guid AddressIdPrimary { get; set; }

        public string StreetName { get; set; }
        public Guid PostalOfficeId { get; set; }

        //public IPostalOfficeModel PostalOffice { get; set; }


        // Properties for postal office -------------------
        public Guid PostalOfficeIdPrimary { get; set; }

        public int PostalOfficeNumber { get; set; }
        public string CityName { get; set; }
        public Guid CountryId { get; set; }

        //public ICountryModel Country { get; set; }


        // Properties for room ---------------------------

        public Guid RoomIdPrimary { get; set; }
        public int RoomNumber { get; set; }
        public Guid RoomTypeID { get; set; }
        public int RoomFloor { get; set; }
        public bool IsFree { get; set; }
    }
}
