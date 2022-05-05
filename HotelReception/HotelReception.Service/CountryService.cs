using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model.Common;
using HotelReception.Repository.Common;
using HotelReception.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Service
{
    public class CountryService : ICountryService
    {
        public ICountryRepository Repository { get; set; }

        public CountryService(ICountryRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<ICountryModel>> GetAllCountriesService(Sorting sorting, Paging paging, CountryFiltering filtering)
        {
            return await Repository.GetAllCountriesAsync(sorting, paging, filtering);
        }
    }
}
