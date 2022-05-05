using HotelReception.Model;
using HotelReception.Service;
using HotelReception.WebApi.Models;
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
    public class RoomTypeController : ApiController
    {

        public async Task<HttpResponseMessage> GetAll()
        {
            RoomTypeService roomTypeService = new RoomTypeService();

            if (roomTypeService.GetAll() == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                List<RESTRoomType> restRoomTypes = new List<RESTRoomType>();
                foreach (RoomTypeModel roomType in await roomTypeService.GetAll())
                {
                    RESTRoomType restRoomType = new RESTRoomType()
                    {
                        Id = roomType.Id,
                        Description = roomType.Description,
                        HasBalcony = roomType.HasBalcony,
                        NumberOfBeds = roomType.NumberOfBeds
                    };
                    restRoomTypes.Add(restRoomType);
                }
                return Request.CreateResponse(HttpStatusCode.OK, restRoomTypes);
            }
        }

        public async Task<HttpResponseMessage> GetIdAsync(Guid id)
        {
            RoomTypeService roomTypeService = new RoomTypeService();

            if (await roomTypeService.GetRoomTypeById(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                RESTRoomType restRoomTypes = new RESTRoomType
                {
                    Id = (await roomTypeService.GetRoomTypeById(id)).Id,
                    Description = (await roomTypeService.GetRoomTypeById(id)).Description,
                    HasBalcony = (await roomTypeService.GetRoomTypeById(id)).HasBalcony,
                    NumberOfBeds = (await roomTypeService.GetRoomTypeById(id)).NumberOfBeds

                };
                return Request.CreateResponse(HttpStatusCode.OK, restRoomTypes);
            }
        }

        public async Task<HttpResponseMessage> PostColumnAsync([FromBody] RESTRoomType restRoomTypes)
        {
            RoomTypeService roomTypeService = new RoomTypeService();
            RoomTypeModel roomType = new RoomTypeModel()
            {
                Id = restRoomTypes.Id,
                Description = restRoomTypes.Description,
                HasBalcony = restRoomTypes.HasBalcony,
                NumberOfBeds = restRoomTypes.NumberOfBeds
            };
            await roomTypeService.PostColumnAsync(roomType);
            return Request.CreateResponse(HttpStatusCode.OK, "New roomType added");
        }

        public async Task<HttpResponseMessage> NewRoomType(Guid id, [FromBody] RESTRoomType restRoomTypes)
        {
            RoomTypeService roomTypeService = new RoomTypeService();

            if (await roomTypeService.GetRoomTypeById(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                RoomTypeModel roomType = new RoomTypeModel
                {
                    Id = restRoomTypes.Id,
                    Description = restRoomTypes.Description,
                    HasBalcony = restRoomTypes.HasBalcony,
                    NumberOfBeds = restRoomTypes.NumberOfBeds
                };

                await roomTypeService.NewRoomType(id, roomType);

                return Request.CreateResponse(HttpStatusCode.OK, $"RoomType with '{id}' updated");
            }



        }
        public async Task<HttpResponseMessage> DeleteRoomType(Guid id)
        {
            RoomTypeService roomTypeService = new RoomTypeService();

            if (await roomTypeService.GetRoomTypeById(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "RoomType not found.");
            }
            else
            {
                await roomTypeService.DeleteRoomTypeById(id);

                return Request.CreateResponse(HttpStatusCode.OK, $"RoomType '{id}'; deleted");
            }
        }

    }
}