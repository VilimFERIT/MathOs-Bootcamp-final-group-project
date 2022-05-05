using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Repository.Common
{
    public interface ICountryRepository
    {
        Task<List<ICountryModel>> GetAllCountriesAsync(Sorting sorting, Paging paging, CountryFiltering filtering);
    }
}
