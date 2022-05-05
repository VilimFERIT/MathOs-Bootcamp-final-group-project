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
    public class PostalOfficeService : IPostalOfficeService
    {
        public IPostalOfficeRepository Repository { get; set; }

        public PostalOfficeService(IPostalOfficeRepository repository)
        {
            Repository = repository;
        }

        public async Task InsertNewPostalOfficeAsyncService(ICountryModel country, IPostalOfficeModel newPostalOffice)
        {
             await Repository.InsertNewPostalOfficeAsync(country, newPostalOffice);
        }
    }
}
