using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Common.GetRoomParameters;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Repository
{
    
    public class RoomRepository : IRoomRepository
    {
        //static string connectionString = @"Data Source=MARIN\SQLEXPRESS01;Initial Catalog=HotelDatabase;Integrated Security=True";
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));

        public async Task<List<AvailableRoom>> GetRoomsAsync(RoomSorting sorting, Paging paging, RoomFiltering roomFiltering)
        {
            StringBuilder queryString = new StringBuilder($"SELECT Room.Id, Room.Number, Room.RoomFloor, Room.Price, RoomType.Description, RoomType.HasBalcony, RoomType.NumberOfBeds" +
                $" FROM Room LEFT JOIN RoomType ON Room.RoomTypeId = RoomType.Id ");

            if (roomFiltering != null)
            {
                queryString.Append($"WHERE 1=1 ");

                if (roomFiltering.Number != null)
                {
                    queryString.Append($"AND Number = {roomFiltering.Number} ");
                }

                if (roomFiltering.Price != null)
                {
                    queryString.Append($"AND Price = {roomFiltering.Price} ");
                }

                if (roomFiltering.Floor != null)
                {
                    queryString.Append($"AND RoomFloor = {roomFiltering.Floor} ");
                }

                if (roomFiltering.Description != null)
                {
                    queryString.Append($"AND RoomType.Description = '{roomFiltering.Description}' ");
                }

                if (roomFiltering.HasBalcony != null)
                {
                    queryString.Append($"AND RoomType.HasBalcony = {roomFiltering.HasBalcony} ");
                }

                if (roomFiltering.NumberOfBeds != null)
                {
                    queryString.Append($"AND RoomType.NumberOfBeds = {roomFiltering.NumberOfBeds} ");
                }

                if (roomFiltering.StartDate != null && roomFiltering.EndDate != null)
                {
                    queryString.Append($"AND Room.Id NOT IN (SELECT RoomId FROM RoomReservation WHERE ('{roomFiltering.StartDate.ToString("yyyy-MM-dd")}' <= EndDate AND '{roomFiltering.EndDate.ToString("yyyy-MM-dd")}' >= StartDate) AND IsActive = 1) ");
                }

            }

            if (sorting != null)
            {
                queryString.Append($"ORDER BY {sorting.SortBy} {sorting.SortOrder} ");
            }

            if (paging != null)
            {
                queryString.Append($"OFFSET ({paging.PageNumber} - 1) * {paging.PageSize} ROWS FETCH NEXT {paging.PageSize} ROWS ONLY ");
            }


            SqlCommand command = new SqlCommand(queryString.ToString(), connection);

            await connection.OpenAsync();

            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                List<AvailableRoom> listOfAvailableRooms = new List<AvailableRoom>();
                while (await reader.ReadAsync())
                {
                    AvailableRoom availableRoom = new AvailableRoom
                    {
                        Id = (Guid)reader["Id"],
                        Number = (int)reader["Number"],
                        RoomFloor = (int)reader["RoomFloor"],
                        Price = (decimal)reader["Price"],
                        Description = (string)reader["Description"],
                        HasBalcony = (bool)reader["HasBalcony"],
                        NumberOfBeds = (int)reader["NumberOfBeds"]
                    };

                    listOfAvailableRooms.Add(availableRoom);

                }
                connection.Close();
                return listOfAvailableRooms;
            }
            else
            {
                return null;
            }

            /*
            StringBuilder queryString = new StringBuilder($"SELECT * FROM Room ");

            if (roomFiltering != null)
            {
                queryString.Append($"WHERE 1=1 ");

                if (roomFiltering.Id != null)
                {
                    queryString.Append($"AND Id = {roomFiltering.Id} ");
                }
                if (roomFiltering.Number != null)
                {
                    queryString.Append($"AND Number = {roomFiltering.Number} ");
                }
                if (roomFiltering.RoomTypeID != null)
                {
                    queryString.Append($"AND PaymentMethod = {roomFiltering.RoomTypeID} ");
                }
                if (roomFiltering.RoomFloor != null)
                {
                    queryString.Append($"AND ReservationId = {roomFiltering.RoomFloor} ");
                }
            }

            if (roomSorting != null)
            {
                queryString.Append($"ORDER BY {roomSorting.SortBy} {roomSorting.SortOrder} ");
            }

            if (roomPaging.PageNumber > 0 && roomPaging.PageSize > 0)
            {
                queryString.Append($"OFFSET ({roomPaging.PageNumber} - 1) * {roomPaging.PageSize} ROWS FETCH NEXT {roomPaging.PageSize} ROWS ONLY ");
            }


            SqlCommand command = new SqlCommand(queryString.ToString(), connection);

            connection.Open();

            SqlDataReader datareader = command.ExecuteReader();

            if (datareader.HasRows)
            {
                List<RoomModel> roomlist = new List<RoomModel>();

                while (datareader.Read())
                {
                    RoomModel rooms = new RoomModel
                    {
                        Id = datareader.GetGuid(0),
                        Number = datareader.GetInt32(1),
                        RoomTypeID = datareader.GetGuid(2),
                        RoomFloor = datareader.GetInt32(3)
                    };

                    roomlist.Add(rooms);
                }
                connection.Close();
                return roomlist;
            }
            else
            {
                return null;
            }
            */
        }
    

    public async Task<List<IRoomModel>> GetRoomByIdAsync(Guid roomId)
    {

        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));

        using (SqlCommand command = new SqlCommand($"SELECT * FROM Room WHERE Id ='{roomId}'", connection))
        {
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            List<IRoomModel> rooms = new List<IRoomModel>();

            while (await reader.ReadAsync())
            {
                RoomModel product = new RoomModel();
                product.Id = reader.GetGuid(0);
                product.Number = reader.GetInt32(1);
                product.RoomTypeID = reader.GetGuid(2);
                product.RoomFloor = reader.GetInt32(3);
                product.IsFree = reader.GetBoolean(4);
                product.Price=reader.GetDecimal(5);
                rooms.Add(product);
            }
            return rooms;
        }

    }


    }




    //public class RoomRepository : IRoomRepository
    //{
    //    public List<RoomModel> rooms = new List<RoomModel>();
    //    static string connectionString = @"Data Source=ST-01\MSSQLSERVER01;Initial Catalog=hotel;Integrated Security=True";



    //    public async Task<List<RoomModel>> GetAllRooms()
    //    {
    //        SqlConnection connection = new SqlConnection(connectionString);
    //        using (connection)
    //        {
    //            SqlCommand command = new SqlCommand("SELECT * FROM Room;", connection);
    //            await connection.OpenAsync();
    //            SqlDataReader reader = command.ExecuteReader();
    //            if (reader.HasRows)
    //            {
    //                List<RoomModel> result = new List<RoomModel>();
    //                while (await reader.ReadAsync())
    //                {
    //                    var room = new RoomModel();
    //                    room.Id = reader.GetGuid(0);
    //                    room.Number = reader.GetInt32(1);
    //                    room.RoomTypeID = reader.GetGuid(1);
    //                    room.RoomFloor = reader.GetInt32(2);
    //                    rooms.Add(room);
    //                }
    //                connection.Close();
    //                reader.Close();
    //                return result;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //    }

    //    //SORTING ROOM


    //    //GET BY ROOM FLOOR

    //    public async Task<List<RoomModel>> GetRoomByFloor(int roomFloor)
    //    {
    //        SqlConnection connection = new SqlConnection(connectionString);

    //        using (connection)
    //        {
    //            SqlCommand command = new SqlCommand($"SELECT * FROM Room WHERE RoomFloor = '{roomFloor}';", connection);
    //            await connection.OpenAsync();
    //            SqlDataReader datareader = await command.ExecuteReaderAsync();

    //            if (datareader.HasRows)
    //            {
    //                List<RoomModel> room = new List<RoomModel>();
    //                while (await datareader.ReadAsync())
    //                {
    //                    var roomtype = new RoomModel();
    //                    roomtype.Id = datareader.GetGuid(0);
    //                    roomtype.Number = datareader.GetInt32(1);
    //                    roomtype.RoomTypeID = datareader.GetGuid(2);
    //                    roomtype.RoomFloor = datareader.GetInt32(3);
    //                    room.Add(roomtype);
    //                }
    //                datareader.Close();
    //                return room;
    //            }
    //            else
    //            {
    //                datareader.Close();
    //                return null;
    //            }
    //        }
    //    }

    //    public async Task<List<RoomModel>> GetRoomByNumber(int number)
    //    {
    //        SqlConnection connection = new SqlConnection(connectionString);

    //        using (connection)
    //        {
    //            SqlCommand command = new SqlCommand($"SELECT * FROM Room WHERE Number = '{number}';", connection);
    //            await connection.OpenAsync();
    //            SqlDataReader datareader = await command.ExecuteReaderAsync();

    //            if (datareader.HasRows)
    //            {
    //                List<RoomModel> room = new List<RoomModel>();
    //                while (await datareader.ReadAsync())
    //                {
    //                    var roomtype = new RoomModel();
    //                    roomtype.Id = datareader.GetGuid(0);
    //                    roomtype.Number = datareader.GetInt32(1);
    //                    roomtype.RoomTypeID = datareader.GetGuid(2);
    //                    roomtype.RoomFloor = datareader.GetInt32(3);
    //                    room.Add(roomtype);
    //                }
    //                datareader.Close();
    //                return room;
    //            }
    //            else
    //            {
    //                datareader.Close();
    //                return null;
    //            }
    //        }
    //    }

    //    //GET BY ROOM ID

    //    public async Task<List<RoomModel>> GetRoomById(Guid id)
    //    {
    //        SqlConnection connection = new SqlConnection(connectionString);

    //        using (connection)
    //        {
    //            SqlCommand command = new SqlCommand($"SELECT * FROM Room WHERE ID = '{id}';", connection);
    //            await connection.OpenAsync();
    //            SqlDataReader datareader = await command.ExecuteReaderAsync();

    //            if (datareader.HasRows)
    //            {
    //                List<RoomModel> room = new List<RoomModel>();
    //                while (await datareader.ReadAsync())
    //                {
    //                    var roomtype = new RoomModel();
    //                    roomtype.Id = datareader.GetGuid(0);
    //                    roomtype.Number = datareader.GetInt32(1);
    //                    roomtype.RoomTypeID = datareader.GetGuid(3);
    //                    roomtype.RoomFloor = datareader.GetInt32(3);
    //                    room.Add(roomtype);
    //                }
    //                datareader.Close();
    //                return room;
    //            }
    //            else
    //            {
    //                datareader.Close();
    //                return null;
    //            }
    //        }
    //    }

    //    //GET BY ROOM TYPE ID

    //    public async Task<List<RoomModel>> GetByRoomTypeID(Guid roomTypeID)
    //    {
    //        SqlConnection connection = new SqlConnection(connectionString);

    //        using (connection)
    //        {
    //            SqlCommand command = new SqlCommand($"SELECT * FROM Room WHERE RoomTypeID = '{roomTypeID}';", connection);
    //            await connection.OpenAsync();
    //            SqlDataReader datareader = await command.ExecuteReaderAsync();

    //            if (datareader.HasRows)
    //            {
    //                List<RoomModel> room = new List<RoomModel>();
    //                while (await datareader.ReadAsync())
    //                {
    //                    var roomtype = new RoomModel();
    //                    roomtype.Id = datareader.GetGuid(0);
    //                    roomtype.Number = datareader.GetInt32(1);
    //                    roomtype.RoomTypeID = datareader.GetGuid(2);
    //                    roomtype.RoomFloor = datareader.GetInt32(3);
    //                    room.Add(roomtype);
    //                }
    //                datareader.Close();
    //                return room;
    //            }
    //            else
    //            {
    //                datareader.Close();
    //                return null;
    //            }
    //        }
    //    }



    //public async Task<RoomModel> GetRoomById(string id)
    //{
    //    SqlConnection connection = new SqlConnection(connectionString);

    //    using (connection)
    //    {
    //        SqlCommand command = new SqlCommand($"SELECT * FROM Room WHERE RoomTypeID = '{id}';", connection);
    //        await connection.OpenAsync();
    //        SqlDataReader reader = await command.ExecuteReaderAsync();

    //        if (reader.Read())
    //        {
    //            RoomModel room = new RoomModel
    //            {
    //                ID = reader.GetInt32(0),
    //                RoomTypeID = reader.GetString(1),
    //                RoomFloor = reader.GetInt32(2)
    //            };
    //            reader.Close();
    //            return room;

    //        }
    //        else
    //        {
    //            reader.Close();
    //            return null;
    //        }


    //    }
    //}


    //    public async Task PostColumnAsync(RoomModel room)
    //{
    //    SqlConnection connection = new SqlConnection(connectionString);
    //    string newColumn = $"INSERT Room(ID, RoomTypeID, RoomFloor) VALUES" +
    //            $"('{room.ID}'," +
    //            $"'{room.RoomTypeID}'," +
    //            $"'{room.RoomFloor}')";

    //    using (connection)
    //    {
    //        SqlCommand command = new SqlCommand(newColumn, connection);
    //        await connection.OpenAsync();
    //        SqlDataAdapter adapter = new SqlDataAdapter(newColumn, connection);

    //        await command.ExecuteNonQueryAsync();
    //        connection.Close();
    //    }

    //}

    //public async Task NewRoom(int id, RoomModel room)
    //{
    //    if (await this.GetRoomById(id) == null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        SqlConnection connection = new SqlConnection(connectionString);
    //        string updateColumn = $"UPDATE Room SET ID = '{room.ID}',RoomTypeID = '{room.RoomTypeID}'," +
    //            $"RoomFloor = '{room.RoomFloor}'";

    //        using (connection)
    //        {
    //            SqlCommand command = new SqlCommand(updateColumn, connection);
    //            await connection.OpenAsync();
    //            SqlDataAdapter adapter = new SqlDataAdapter(updateColumn, connection);

    //            await command.ExecuteNonQueryAsync();
    //            connection.Close();
    //        }

    //    }
    //}








    //public async Task DeleteRoomById(int Id)
    //{
    //    if (await this.GetRoomById(Id) == null)
    //    {
    //        return;
    //    }

    //    SqlConnection connection = new SqlConnection(connectionString);
    //    string deleteColumn = $"DELETE FROM Room WHERE ID = '{Id}';";

    //    using (connection)
    //    {
    //        SqlCommand command = new SqlCommand(deleteColumn, connection);
    //        await connection.OpenAsync();
    //        SqlDataAdapter adapter = new SqlDataAdapter(deleteColumn, connection);

    //        await command.ExecuteNonQueryAsync();
    //        connection.Close();
    //    }
    //}
}
    

