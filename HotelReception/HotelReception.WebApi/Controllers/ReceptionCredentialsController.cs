using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Service;
using HotelReception.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HotelReception.WebApi.Controllers
{
    public class ReceptionCredentialsController : ApiController
    {
        protected IReceptionCredentialsService Service { get; set; }
        public ReceptionCredentialsController(IReceptionCredentialsService service)
        {
            Service = service;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<HttpResponseMessage> GetLoginCredentials([FromBody]ReceptionCredentialsModel credentials)
        {

            IReceptionistModel receptionist = new ReceptionistModel();
            receptionist = await Service.GetLoginCredentials(credentials);
            if (receptionist != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, receptionist);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
           
        }
    }
}