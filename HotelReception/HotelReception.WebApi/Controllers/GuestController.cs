using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Service.Common;
using HotelReception.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HotelReception.WebApi.Controllers
{
    public class GuestController : ApiController
    {
        protected IGuestService Service { get; private set; }

        public GuestController(IGuestService service)
        {
            Service = service;
        }

        //GET: Guest

        [HttpGet]
        [Route("guest/getactiveguests")]

        public async Task<HttpResponseMessage> GetActiveGuests([FromUri]Sorting sorting, [FromUri]Paging paging, [FromUri]ActiveGuestFiltering filtering)
        {

            var activeGuests = await Service.GetActiveGuests(sorting,paging, filtering);
            if(activeGuests!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, activeGuests);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]

        [Route("guest/getall")]

        public async Task<HttpResponseMessage> GetAllGuestsAsync([FromUri] Sorting sorting, [FromUri] Paging paging, [FromUri] GuestFiltering filtering)
        {
            var guests = await Service.GetAllGuestsServiceAsync(sorting, paging, filtering);
            List<GuestRest> guestsRest = new List<GuestRest>();

            if (guests == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                foreach (var guest in guests)
                {
                    GuestRest guestRest = new GuestRest();
                    guestRest.Id = guest.Id;
                    guestRest.Pid = guest.Pid;
                    guestRest.FirstName = guest.FirstName;
                    guestRest.LastName = guest.LastName;
                    guestRest.RoomId = guest.RoomId;
                    guestRest.AddressId = guest.AddressId;
                    guestRest.IsActive = guest.IsActive;
                    guestsRest.Add(guestRest);
                }

                return Request.CreateResponse(HttpStatusCode.OK, guestsRest);
            }
        }


        // POST: Guest
        
        [HttpPost]

        [Route("guest/addnew")]
        public async Task<HttpResponseMessage> InsertNewGuestAsync(GuestInsert guestInsert)
        {
            Guid postalOfficeGuid = Guid.NewGuid();
            Guid addressGuid = Guid.NewGuid();
            Guid guestGuid = Guid.NewGuid();
            if (guestInsert != null)
            {
                InsertGuestDomain newGuest = new InsertGuestDomain
                {
                    // COUNTRY INFO
                    Name = guestInsert.Name,


                    // POSTAL OFFICE INFO
                    PostalOfficeIdPrimary = postalOfficeGuid,
                    PostalOfficeNumber = guestInsert.PostalOfficeNumber,
                    CityName = guestInsert.CityName,


                    // ADDRESS INFO
                    AddressIdPrimary = addressGuid,
                    StreetName = guestInsert.StreetName,
                    PostalOfficeId = postalOfficeGuid,


                    // GUEST INFO
                    GuestIdPrimary = guestGuid,
                    Pid = guestInsert.Pid,
                    FirstName = guestInsert.FirstName,
                    LastName = guestInsert.LastName,
                    AddressId = addressGuid,


                    // ROOM INFO
                    RoomNumber = guestInsert.RoomNumber,
                };

                await Service.InsertNewGuestAsync(newGuest);
                return Request.CreateResponse(HttpStatusCode.OK, "A new guest has been added!");

                
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You're trying to add an empty object!");
            }
        }


        // PUT: Guest
        [HttpPut]

        [Route("guest/activity")]

        public async Task<HttpResponseMessage> ChangeGuestActivityAsync([FromUri] Guid id)
        {
            if (id != null)
            {
                await Service.ChangeActivityStatus(id);
                return Request.CreateResponse(HttpStatusCode.OK, "The guest's activity has been changed!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]

        [Route("guest/activitycheck")]

        public async Task<HttpResponseMessage> ChangeActivityStatusCheckAsync([FromBody] List<ActiveGuestModel> activeGuests)
        {
            if(activeGuests != null)
            {
                await Service.ChangeActivityStatusCheckAsync(activeGuests);
                return Request.CreateResponse(HttpStatusCode.OK, "The guest's activity has been changed!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        public class GuestInsert
        {
            // Properties for guest -------------------------
            public Guid GuestIdPrimary { get; set; } 
            public string Pid { get; set; }

            public string FirstName { get; set; }
            
            public string LastName { get; set; }

            public Guid RoomId { get; set; }
            public Guid AddressId { get; set; }
            public bool IsActive { get; set; }



            // Properties for country ----------------
            public Guid CountryIdPrimary { get; set; }
            
            public string Name { get; set; }



            // Properties for address ------------------
            public Guid AddressIdPrimary { get; set; }

            public string StreetName { get; set; }
            public Guid PostalOfficeId { get; set; }



            // Properties for postal office -------------------
            public Guid PostalOfficeIdPrimary { get; set; }

            public int PostalOfficeNumber { get; set; }
            public string CityName { get; set; }
            public Guid CountryId { get; set; }



            // Properties for room ---------------------------
            public Guid RoomIdPrimary { get; set; }
            public int RoomNumber { get; set; }
            public Guid RoomTypeID { get; set; }
            public int RoomFloor { get; set; }
        }

    }
}
