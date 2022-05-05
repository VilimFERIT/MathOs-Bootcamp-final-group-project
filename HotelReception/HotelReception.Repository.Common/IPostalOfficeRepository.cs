using HotelReception.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Repository.Common
{
    public interface IPostalOfficeRepository
    {
        Task InsertNewPostalOfficeAsync(ICountryModel newCountry, IPostalOfficeModel newPostalOffice);
    }
}
