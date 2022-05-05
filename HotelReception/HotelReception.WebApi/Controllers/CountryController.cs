using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
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
    public class CountryController : ApiController
    {
        protected ICountryService Service { get; private set; }

        public CountryController(ICountryService service)
        {
            Service = service;
        }
        
        // GET: api/Countryasd
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Country/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Country
        [HttpGet]
        [Route("api/getcountries")]
        public async Task<HttpResponseMessage> GetAllCountries([FromUri]Sorting sorting, [FromUri] Paging paging,  [FromUri] CountryFiltering filtering)
        {

            var countries =  await Service.GetAllCountriesService(sorting, paging, filtering);
            return Request.CreateResponse(HttpStatusCode.OK, countries);
            
        }

        // PUT: api/Country/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Country/51
        public void Delete(int id)
        {
        }
    }
}
