using HotelReception.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model
{
    public class AddressModel : IAddressModel
    {
        public Guid Id { get; set; }

        public string StreetName { get; set; }
        public Guid PostalOfficeId { get; set; }

        public IPostalOfficeModel PostalOffice { get; set; }

    }
}
