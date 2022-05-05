using HotelReception.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model
{
    public class PostalOfficeModel : IPostalOfficeModel
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
        public string CityName { get; set; }
        public Guid CountryId { get; set; }

        public ICountryModel Country { get; set; }
    }
}
