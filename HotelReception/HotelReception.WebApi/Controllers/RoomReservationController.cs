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
    public class RoomReservationController : ApiController
    {
        protected IRoomReservationService RoomReservationService { get; set; }

        public RoomReservationController(IRoomReservationService roomReservationService)
        {
            RoomReservationService = roomReservationService;
        }


        [HttpGet]
        [Route("api/GetRoomReservations")]
        public async Task<HttpResponseMessage> GetRoomReservations([FromUri] RoomReservationSorting sorting, [FromUri] Paging paging, [FromUri] RoomReservationFiltering roomReservationFiltering)
        {
            List<IRoomReservation> listOfAvailableRooms = await RoomReservationService.GetRoomReservationsAsync(sorting, paging, roomReservationFiltering);

            if (listOfAvailableRooms != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, listOfAvailableRooms);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Not found");
            }

            
        }

        
        [HttpPost]
        [Route("api/PostRoomReservation")]
        public async Task<HttpResponseMessage> PostRoomReservationAsync(RoomReservationRestInsert roomReservationRestInsert)
        {
            if (roomReservationRestInsert != null)
            {
                RoomReservation roomRes = new RoomReservation
                {
                    RoomId = roomReservationRestInsert.RoomId,
                    ReservationId = roomReservationRestInsert.ReservationId,
                    StartDate = roomReservationRestInsert.StartDate,
                    EndDate = roomReservationRestInsert.EndDate
                };

                await RoomReservationService.PostRoomReservationAsync(roomRes);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Invalid input");
            }
        }

        // PUT: api/RoomReservation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RoomReservation/5
        public void Delete(int id)
        {
        }


        public class RoomReservationRestInsert
        {
            public Guid RoomId { get; set; }
            public Guid ReservationId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
