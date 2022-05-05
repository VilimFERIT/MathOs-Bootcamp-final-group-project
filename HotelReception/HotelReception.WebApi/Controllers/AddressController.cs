using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HotelReception.WebApi.Controllers
{
    public class AddressController : ApiController
    {

        protected IAddressService Service { get; private set; }

        public AddressController(IAddressService service)
        {
            Service = service;
        }
        // GET: api/Address
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Address/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Address
        [HttpPost]

        [Route("address/addnew")]
        public async Task<HttpResponseMessage> InsertNewAddressAsync(IAddressModel newAddress, IPostalOfficeModel postalOfficeNumber)
        {
            if (newAddress == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You're trying to add an empty object!");
            }
            else
            {
                await Service.InsertNewAddressAsyncService(newAddress,postalOfficeNumber);
                return Request.CreateResponse(HttpStatusCode.OK, "A new address has been added!");
            }
        }

        // PUT: api/Address/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Address/5
        public void Delete(int id)
        {
        }
    }
}
