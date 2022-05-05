using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Service.Common;

namespace HotelReception.WebApi.Controllers
{
    public class PaymentController : ApiController
    {
        protected IPaymentService PaymentService { get; set; }

        public PaymentController(IPaymentService service)
        {
            PaymentService = service;
        }


        [HttpGet]
        [Route("api/getPayments")]
        public async Task<HttpResponseMessage> GetPaymentsAsync([FromUri] Sorting sorting, [FromUri] Paging paging, [FromUri] PaymentFiltering paymentFiltering)
        {
            List<Payment> listOfPayments = await PaymentService.GetPaymentsAsync(sorting, paging, paymentFiltering);

            if (listOfPayments != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, listOfPayments);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Not found");
            }
        }


        /*
        // GET: api/Payment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Payment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Payment
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Payment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Payment/5
        public void Delete(int id)
        {
        }
        */
    }
}
