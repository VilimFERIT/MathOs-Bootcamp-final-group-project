using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository.Common;
using HotelReception.Common;
using HotelReception.Common.GetReservation;
using HotelReception.Common.GetParameters;

namespace HotelReception.Repository
{
    public class RoomReservationRepository : IRoomReservationRepository
    {
        //static string connectionString = @"Data Source=MARIN\SQLEXPRESS01;Initial Catalog=HotelDatabase;Integrated Security=True";
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));


        public async Task<List<IRoomReservation>> GetRoomsAsync(RoomReservationSorting sorting, Paging paging, RoomReservationFiltering roomReservationFiltering)
        {
            StringBuilder queryString = new StringBuilder($"SELECT * FROM RoomReservation ");

            if (roomReservationFiltering != null)
            {
                queryString.Append($"WHERE 1=1 ");

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
                List<IRoomReservation> listOfAvailableRooms = new List<IRoomReservation>();
                while (await reader.ReadAsync())
                {
                    RoomReservation retrievedRoom = new RoomReservation
                    {
                        RoomId = (Guid)reader["RoomId"],
                        ReservationId = (Guid)reader["ReservationId"],
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"]
                    };

                    listOfAvailableRooms.Add(retrievedRoom);

                }
                connection.Close();
                return listOfAvailableRooms;
            }
            else
            {
                return null;
            }
        }


        public async Task PostRoomReservationAsync(RoomReservation roomReservation)
        {
            SqlCommand command = new SqlCommand
                ($"INSERT INTO RoomReservation VALUES ({roomReservation.RoomId}, {roomReservation.ReservationId}, {roomReservation.StartDate}, {roomReservation.EndDate});", connection);

            await connection.OpenAsync();

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = command;
            await adapter.InsertCommand.ExecuteNonQueryAsync();

            connection.Close();
        }
    }
}
