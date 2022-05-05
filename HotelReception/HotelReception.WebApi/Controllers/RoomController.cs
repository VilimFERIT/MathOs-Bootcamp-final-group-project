using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Common.GetRoomParameters;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Service;
using HotelReception.Service.Common;
using HotelReception.WebApi;
using HotelReception.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HotelReception.WebApi.Controllers
{

    public class RoomController : ApiController
    {

        public IRoomService RoomService { get; set; }

        public RoomController(IRoomService roomService)
        {
            RoomService = roomService;
        }


        // Get free rooms based on time period (from, to)
        [HttpGet]
        [Route("api/GetRooms")]
        public async Task<HttpResponseMessage> GetRoomsAsync([FromUri] RoomSorting sorting, [FromUri] Paging paging, [FromUri] RoomFiltering roomFiltering)
        {

            List<AvailableRoom> listOfAvailableRooms = await RoomService.GetRoomsAsync(sorting, paging, roomFiltering);

            if (listOfAvailableRooms != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, listOfAvailableRooms);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, listOfAvailableRooms);
            }


            /*
            ([FromUri] RoomSorting roomSorting, [FromUri] RoomPaging roomPaging, [FromUri] RoomFiltering roomFiltering)
            List<RoomModel> roomlist = RoomService.GetRooms(roomSorting, roomPaging, roomFiltering);

            if (roomlist != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, roomlist);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Not found");
            }
            */
        }

        [HttpGet]
        [Route("api/GetRoomById")]
        public async Task<HttpResponseMessage> GetRoomById(Guid roomId)
        {
            var room = await RoomService.GetRoomById(roomId);

            if (room != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, room);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Not found");
            }
        }

        

        //public class RoomController : ApiController
        //{

        //    public async Task<HttpResponseMessage> GetAllRooms()
        //    {
        //        RoomService roomService = new RoomService();

        //        if (roomService.GetAllRooms() == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //        }
        //        else
        //        {
        //            List<RESTRoom> restRooms = new List<RESTRoom>();
        //            foreach (RoomModel room in await roomService.GetAllRooms())
        //            {
        //                RESTRoom restRoom = new RESTRoom()
        //                {
        //                    Id = room.Id,
        //                    Number = room.Number,
        //                    RoomTypeID = room.RoomTypeID,
        //                    RoomFloor = room.RoomFloor
        //                };
        //                restRooms.Add(restRoom);
        //            }
        //            return Request.CreateResponse(HttpStatusCode.OK, restRooms);
        //        }
        //    }

        //    //FILTER BY ROOM FLOOR

        //    [HttpGet]
        //    [Route("api/room/floor/{roomFloor}")]
        //    public async Task<HttpResponseMessage> GetRoomByFloor(int roomFloor)
        //    {
        //        RoomService roomService = new RoomService();

        //        if (await roomService.GetRoomByFloor(roomFloor) == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //        }
        //        else
        //        {
        //            List<RoomModel> rooms = new List<RoomModel>();
        //            rooms = await roomService.GetRoomByFloor(roomFloor);    
        //            return Request.CreateResponse(HttpStatusCode.OK, rooms);
        //        }
        //    }

        //    //FILTER BY ROOM NUMBER

        //    [HttpGet]
        //    [Route("api/room/number/{number}")]
        //    public async Task<HttpResponseMessage> GetRoomByNumber(int number)
        //    {
        //        RoomService roomService = new RoomService();

        //        if (await roomService.GetRoomByNumber(number) == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //        }
        //        else
        //        {
        //            List<RoomModel> rooms = new List<RoomModel>();
        //            rooms = await roomService.GetRoomByNumber(number);
        //            return Request.CreateResponse(HttpStatusCode.OK, rooms);
        //        }
        //    }

        //    //FILTER BY ROOM ID

        //    [HttpGet]
        //    [Route("api/room/id/{id}")]
        //    public async Task<HttpResponseMessage> GetRoomById(Guid id)
        //    {
        //        RoomService roomService = new RoomService();

        //        if (await roomService.GetRoomById(id) == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //        }
        //        else
        //        {
        //            List<RoomModel> rooms = new List<RoomModel>();
        //            rooms = await roomService.GetRoomById(id);
        //            return Request.CreateResponse(HttpStatusCode.OK, rooms);
        //        }
        //    }

        //    [HttpGet]
        //    [Route("api/room/roomtype/{roomTypeID}")]
        //    public async Task<HttpResponseMessage> GetByRoomTypeID(Guid roomTypeID)
        //    {
        //        RoomService roomService = new RoomService();

        //        if (await roomService.GetByRoomTypeID(roomTypeID) == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //        }
        //        else
        //        {
        //            List<RoomModel> rooms = new List<RoomModel>();
        //            rooms = await roomService.GetByRoomTypeID(roomTypeID);
        //            return Request.CreateResponse(HttpStatusCode.OK, rooms);
        //        }
        //    }


        //public async Task<HttpResponseMessage> GetIdAsync(string id)
        //{
        //    RoomService roomService = new RoomService();

        //    if (await roomService.GetRoomById(id) == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }
        //    else
        //    {
        //        RoomModel restRooms = new RoomModel
        //        {
        //            ID = (await roomService.GetRoomById(id)).ID,
        //            RoomTypeID = (await roomService.GetRoomById(id)).RoomTypeID,
        //            RoomFloor = (await roomService.GetRoomById(id)).RoomFloor

        //        };
        //        return Request.CreateResponse(HttpStatusCode.OK, restRooms);
        //    }
        //}


        //public async Task<HttpResponseMessage> PostColumnAsync([FromBody] RESTRoom restRooms)
        //{
        //    RoomService roomService = new RoomService();
        //    RoomModel room = new RoomModel()
        //    {
        //        ID = restRooms.ID,
        //        RoomTypeID = restRooms.RoomTypeID,
        //        RoomFloor = restRooms.RoomFloor
        //    };
        //    await roomService.PostColumnAsync(room);
        //    return Request.CreateResponse(HttpStatusCode.OK, "New room added");
        //}

        //public async Task<HttpResponseMessage> NewRoom(int id, [FromBody] RESTRoom restRooms)
        //{
        //    RoomService roomService = new RoomService();

        //    if (await roomService.GetRoomById(id) == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }
        //    else
        //    {
        //        RoomModel room = new RoomModel
        //        {
        //            ID = restRooms.ID,
        //            RoomTypeID = restRooms.RoomTypeID,
        //            RoomFloor = restRooms.RoomFloor
        //        };

        //        await roomService.NewRoom(id, room);

        //        return Request.CreateResponse(HttpStatusCode.OK, $"Room with '{id}' updated");
        //    }



        //}
        //public async Task<HttpResponseMessage> DeleteRoom(int id)
        //{
        //    RoomService roomService = new RoomService();

        //    if (await roomService.GetRoomById(id) == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound, "Room not found.");
        //    }
        //    else
        //    {
        //        await roomService.DeleteRoomById(id);

        //        return Request.CreateResponse(HttpStatusCode.OK, $"Room '{id}'; deleted");
        //    }
        //}

    }
}