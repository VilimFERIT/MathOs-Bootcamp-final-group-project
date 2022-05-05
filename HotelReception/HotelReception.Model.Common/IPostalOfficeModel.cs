using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model.Common
{
    public interface IPostalOfficeModel
    {
        Guid Id { get; set; }
        string CityName { get; set; }
        Guid CountryId { get; set; }

        int Number { get; set; }

        ICountryModel Country { get; set; }

    }
}
