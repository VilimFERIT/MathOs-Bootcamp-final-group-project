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
using HotelReception.Model.Common;
using HotelReception.Service.Common;

namespace HotelReception.WebApi.Controllers
{
    public class ReceiptController : ApiController
    {

        protected IReceiptService ReceiptService { get; set; }

        public ReceiptController(IReceiptService receiptService)
        {
            ReceiptService = receiptService;
        }


        [HttpGet]
        [Route("api/GetReceipts")]
        public async Task<HttpResponseMessage> GetReceiptsAsync([FromUri] Sorting sorting, [FromUri] Paging paging, [FromUri] ReceiptFiltering receiptFiltering)
        {
            List<IReceipt> listOfReceipts = new List<IReceipt>();

            if (sorting == null && paging == null && receiptFiltering == null)
            {
                Sorting sort = new Sorting();
                Paging page = new Paging();

                listOfReceipts = await ReceiptService.GetReceiptsAsync(sort, page, receiptFiltering);
            }
            else
            {
                listOfReceipts = await ReceiptService.GetReceiptsAsync(sorting, paging, receiptFiltering);
            }

            

            if (listOfReceipts != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, listOfReceipts);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Not found");
            }
            
        }

        /*
        // POST: api/Receipt
        [HttpPost]
        [Route("api/PostReceipt")]
        public async Task<HttpResponseMessage> PostReceiptAsync(ReceiptRestInsert receiptRestInsert)
        {
            if (receiptRestInsert != null)
            {
                Receipt receipt = new Receipt
                {
                    Id = Guid.NewGuid(),
                    Price = receiptRestInsert.Price,
                    PaymentMethod = receiptRestInsert.PaymentMethod,
                    ReservationID = receiptRestInsert.ReservationId
                };

                await ReceiptService.PostReceiptAsync(receipt);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Invalid input");
            }
        }
        */


        /*
        public class ReceiptRestInsert
        {
            public int Id { get; set; }
            public decimal Price { get; set; }
            public string PaymentMethod { get; set; }
            public int ReservationId { get; set; }
        }
        */
    }
}
