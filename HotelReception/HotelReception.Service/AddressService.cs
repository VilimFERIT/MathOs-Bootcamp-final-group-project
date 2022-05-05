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
    public class AddressService :IAddressService
    {
        public IAddressRepository Repository { get; set; }

        public AddressService(IAddressRepository repository)
        {
            Repository = repository;
        }
        public async Task InsertNewAddressAsyncService(IAddressModel newAddress, IPostalOfficeModel postalOfficeNumber)
        {
            await Repository.InsertNewAddressAsync(newAddress, postalOfficeNumber);
        }
    }
}
