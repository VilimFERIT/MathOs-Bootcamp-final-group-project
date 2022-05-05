using HotelReception.Model;
using HotelReception.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public List<RoomTypeModel> roomTypes = new List<RoomTypeModel>();
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
        //static string connectionString = @"Data Source=ST-01\MSSQLSERVER01;Initial Catalog=hotel;Integrated Security=True";


        public async Task<List<RoomTypeModel>> GetAll()
        {

            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM RoomType;", connection);
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<RoomTypeModel> result = new List<RoomTypeModel>();
                    while (await reader.ReadAsync())
                    {
                        var roomtype = new RoomTypeModel();
                        roomtype.Id = reader.GetGuid(1);
                        roomtype.Description = reader.GetString(2);
                        roomtype.HasBalcony = reader.GetBoolean(2);
                        roomtype.NumberOfBeds = reader.GetInt32(3);
                        roomTypes.Add(roomtype);
                        


                    }
                    connection.Close();
                    reader.Close();
                    return result;
                }
                else
                {
                    return null;
                }

            }

        }


        public async Task<RoomTypeModel> GetRoomTypeById(Guid id)
        {
            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));

            using (connection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM RoomType WHERE Id = '{id}';", connection);
                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    RoomTypeModel roomtype = new RoomTypeModel
                    {
                        Id = reader.GetGuid(0),
                        Description = reader.GetString(1),
                        HasBalcony = reader.GetBoolean(1),
                        NumberOfBeds = reader.GetInt32(2)
                    };
                    reader.Close();
                    return roomtype;

                }
                else
                {
                    reader.Close();
                    return null;
                }


            }
        }


        public async Task PostColumnAsync(RoomTypeModel roomtype)
        {
            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
            string newColumn = $"INSERT RoomType(ID, HasBalcony, NumberOfBeds) VALUES" +
                    $"('{roomtype.Id}'," +
                    $"'{roomtype.Description}'," +
                    $"'{roomtype.HasBalcony}'," +
                    $"'{roomtype.NumberOfBeds}')";

            using (connection)
            {
                SqlCommand command = new SqlCommand(newColumn, connection);
                await connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(newColumn, connection);

                await command.ExecuteNonQueryAsync();
                connection.Close();
            }

        }

        public async Task NewRoomType(Guid id, RoomTypeModel roomType)
        {
            if (await this.GetRoomTypeById(id) == null)
            {
                return;
            }
            else
            {
                SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
                string updateColumn = $"UPDATE RoomType SET Id = '{roomType.Id}',HasBalcony = '{roomType.HasBalcony}'," +
                    $"NumberOfBeds = '{roomType.NumberOfBeds}'";

                using (connection)
                {
                    SqlCommand command = new SqlCommand(updateColumn, connection);
                    await connection.OpenAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(updateColumn, connection);

                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }

            }
        }








        public async Task DeleteRoomTypeById(Guid Id)
        {
            if (await this.GetRoomTypeById(Id) == null)
            {
                return;
            }

            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
            string deleteColumn = $"DELETE FROM RoomType WHERE ID = '{Id}';";

            using (connection)
            {
                SqlCommand command = new SqlCommand(deleteColumn, connection);
                await connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(deleteColumn, connection);

                await command.ExecuteNonQueryAsync();
                connection.Close();
            }
        }
    }
}
