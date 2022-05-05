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
    public class PostalOfficeController : ApiController
    {
        protected IPostalOfficeService Service { get; private set; }

        public PostalOfficeController(IPostalOfficeService service)
        {
            Service = service;
        }
        // GET: api/PostalOffice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PostalOffice/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PostalOffice
        public async Task<HttpResponseMessage> InsertNewPostalOffice(ICountryModel country, IPostalOfficeModel newPostalOffice)
        {
            if (country == null || newPostalOffice == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You're trying to add invalid objects");
            }
            else
            {
                await Service.InsertNewPostalOfficeAsyncService(country, newPostalOffice);
                return Request.CreateResponse(HttpStatusCode.OK, "A new city with a new postal office code has been added!");
            }
        }
        // PUT: api/PostalOffice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PostalOffice/5
        public void Delete(int id)
        {
        }
    }
}
