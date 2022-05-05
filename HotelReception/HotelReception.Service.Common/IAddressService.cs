using HotelReception.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Service.Common
{
    public interface IAddressService
    {
        Task InsertNewAddressAsyncService(IAddressModel newAddress, IPostalOfficeModel postalOfficeNumber);
    }
}
