using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Service.Common;

namespace HotelReception.WebApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReservationController : ApiController
    {
        protected IReservationService ReservationService { get; set; }

        public ReservationController(IReservationService service)
        {
            ReservationService = service;
        }
        
        
        [HttpGet]
        [Route("api/GetReservations")]
        public async Task<HttpResponseMessage> GetReservations ([FromUri] Sorting sorting, [FromUri] Paging paging, [FromUri] ReservationFiltering filtering)
        {
            //Convert.ToInt32(filtering.IsActive);
            //int isActive = Convert.ToInt32(filtering.IsActive);
            List<IReservation> listOfReservations = new List<IReservation>();
            List<IGuestModel> guestList = new List<IGuestModel>();

            if (sorting == null && paging == null && filtering == null)
            {
                Sorting sort = new Sorting();
                Paging page = new Paging();

                listOfReservations = await ReservationService.GetReservationsAsync(sort, page, filtering);
            }
            else
            {
                listOfReservations = await ReservationService.GetReservationsAsync(sorting, paging, filtering);
            }


            if (listOfReservations != null)
            {
                List<IReservation> listMapped = new List<IReservation>();
                List<IGuestModel> guestsMapped = new List<IGuestModel>();

                return Request.CreateResponse(HttpStatusCode.OK, listOfReservations);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, listOfReservations);
            }
 
        }

        
        [HttpPost]
        [Route("api/PostReservation")]
        public async Task<HttpResponseMessage> PostReservationAsync(InsertReservationRest insertReservation)
        {
            Guid reservationGuid = Guid.NewGuid();
            Guid receiptGuid = Guid.NewGuid();
            if (insertReservation != null)
            {          
                InsertReservationDomain reservationInfo = new InsertReservationDomain
                {
                    // RESERVATION INFO
                    ResId = reservationGuid,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    

                    // RECEIPT INFO
                    RecId = receiptGuid,
                    Price = insertReservation.Price,
                    PaymentMethod = insertReservation.PaymentMethod,
                    ReservationId = reservationGuid, // foreign key, equal to ResId


                    // ROOMRESERVATION INFO
                    StartDate = insertReservation.StartDate,
                    EndDate = insertReservation.EndDate,


                    // RECEPTIONIST INFO (for retrieving receptionist's Id)
                    FirstName = insertReservation.FirstName,
                    LastName = insertReservation.LastName,


                    // GUEST INFO (for retrieveing guest's Id and RoomId)
                    Pid = insertReservation.Pid,
                };

                await ReservationService.PostReservationAsync(reservationInfo);

                return Request.CreateResponse(HttpStatusCode.OK, $"Reservation inserted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Invalid input");
            }     
        }


        [HttpPut]
        [Route("api/PutReservation")]
        public async Task<HttpResponseMessage> PutReservationAsync(Guid id)
        {
            if (id != null)
            {
                await ReservationService.PutReservationAsync(id);

                return Request.CreateResponse(HttpStatusCode.OK, $"Reservation updated!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Invalid input");
            }
    
        }

    }

    

    public class InsertReservationRest
    {
        // Properties for reservation
        public Guid ResId { get; set; }
        public Guid ReceptionistId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }


        // Properties for receipt
        public Guid RecId { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethod { get; set; }
        public Guid ReservationId { get; set; } // foreign key references ResId
        

        // Properties for retrieving the receptionist
        public string FirstName { get; set; }
        public string LastName { get; set; }


        // Properties for RoomReservation table
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        // Property for retrieving the guest
        public string Pid { get; set; } // --> Personal Identification

        
    }

    
}
