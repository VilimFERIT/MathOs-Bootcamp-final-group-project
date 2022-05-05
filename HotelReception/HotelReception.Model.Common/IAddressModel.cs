using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model.Common
{
    public interface IAddressModel
    {
        Guid Id { get; set; }
        string StreetName { get; set; }
        Guid PostalOfficeId { get; set; }

        IPostalOfficeModel PostalOffice { get; set; }
    }
}
