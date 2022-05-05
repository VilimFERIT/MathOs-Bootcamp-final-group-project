using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
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
    public class GuestRepository : IGuestRepository
    {
        
        //static string connectionString = @"Data Source=MARIN\SQLEXPRESS01;Initial Catalog=HotelDatabase;Integrated Security=True";
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
        public async Task<List<IGuestModel>> GetAllGuestsAsync(Sorting sorting, Paging paging, GuestFiltering filtering)
        {
            StringBuilder queryString = new StringBuilder("SELECT * FROM Guest ");
            
            if (filtering != null)
            {
                queryString.Append("WHERE 1=1 ");

                if (filtering.Pid != null)
                {
                    queryString.Append($"AND Pid = '{filtering.Pid}' ");
                }
                if (filtering.FirstName != null)
                {
                    queryString.Append($"AND FirstName = '{filtering.FirstName}' ");
                }
                if (filtering.LastName != null)
                {
                    queryString.Append($"AND LastName = '{filtering.LastName}' ");
                }
                if (filtering.RoomId != null)
                {
                    queryString.Append($"AND RoomId = '{filtering.RoomId}' ");
                }
                if (filtering.AddressId != null)
                {
                    queryString.Append($"AND AddressId = '{filtering.AddressId}' ");
                }
                if (filtering.IsActive != null)
                {
                    queryString.Append($"AND IsActive = {filtering.IsActive}");
                }
            }

            if (sorting != null)
            {
                queryString.Append($"ORDER BY {sorting.SortBy} {sorting.SortOrder} ");
            }

            if ((paging.PageNumber > 0 && paging.PageSize > 0))
            {
                queryString.Append($"OFFSET ({paging.PageNumber} - 1) * {paging.PageSize} ROWS FETCH NEXT {paging.PageSize} ROWS ONLY ");
            }


            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
            using (connection)
            {
                using (SqlCommand command = new SqlCommand(queryString.ToString(), connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    List<IGuestModel> guests = new List<IGuestModel>();
                    while (await reader.ReadAsync())
                    {
                        GuestModel guest = new GuestModel();
                        guest.Id = (Guid)reader["Id"];
                        guest.Pid = (string)reader["Pid"];
                        guest.FirstName = (string)reader["FirstName"];
                        guest.LastName = (string)reader["LastName"];
                        guest.IsActive = (bool)reader["IsActive"];
                        guest.RoomId = (Guid)reader["RoomId"];
                        guest.AddressId = (Guid)reader["AddressId"];   
                        guests.Add(guest);
                    }
                    return guests;
                }


            }
        }

        public async Task InsertNewGuestAsync(IGuestModel newGuest, ICountryModel country, IAddressModel address, IPostalOfficeModel postalOffice, RoomModel room)
        {
            // Retrieve Country (Id)
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Country WHERE Name='{country.Name}';", connection);
            await connection.OpenAsync();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                
                while (await reader.ReadAsync())
                {
                    country.Id = (Guid)reader["Id"];
                }
            }
            reader.Close();


            // Retrieve Room (Id, RoomTypeId, RoomFloor)
            SqlCommand sqlCommand2 = new SqlCommand($"SELECT * FROM Room WHERE Number = {room.Number};", connection);
            SqlDataReader reader2 = sqlCommand2.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    room.Id = (Guid)reader2["Id"];
                    room.RoomTypeID = (Guid)reader2["RoomTypeId"];
                    room.RoomFloor = (int)reader2["RoomFloor"];
                }
            }
            reader2.Close();


            // Insert info into three tables
            using(connection)
            {

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command1 = new SqlCommand($"INSERT INTO PostalOffice VALUES ('{postalOffice.Id}', {postalOffice.Number}, '{postalOffice.CityName}','{country.Id}')", connection, transaction);
                    command1.ExecuteNonQuery();

                    SqlCommand command2 = new SqlCommand($"INSERT INTO Address VALUES ('{address.Id}', '{address.StreetName}','{postalOffice.Id}')", connection, transaction);
                    command2.ExecuteNonQuery();

                    SqlCommand command3 = new SqlCommand($"INSERT INTO Guest  VALUES ('{newGuest.Id}', '{newGuest.Pid}','{newGuest.FirstName}','{newGuest.LastName}', '{room.Id}','{address.Id}', 1)", connection, transaction);
                    command3.ExecuteNonQuery(); 

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                connection.Close();
            }
        }   

        public async Task<List<IActiveGuestModel>> GetActiveGuests(Sorting sorting, Paging paging, ActiveGuestFiltering filtering)
        {
            StringBuilder queryString = new StringBuilder("SELECT Guest.Id, Guest.FirstName, Guest.LastName, Guest.Pid, Guest.IsActive, Room.Number, Address.StreetName, PostalOffice.CityName, Country.Name FROM Guest INNER JOIN Room ON Guest.RoomId = Room.Id INNER JOIN Address ON Guest.AddressId = Address.Id INNER JOIN PostalOffice ON Address.PostalOfficeId = PostalOffice.Id INNER JOIN Country ON Country.Id = PostalOffice.CountryId ");


            if (filtering != null)
            {
                queryString.Append("WHERE 1=1 ");

                if (filtering.FirstName != null)
                {
                    queryString.Append($"AND FirstName = '{filtering.FirstName}' ");
                }
                if (filtering.LastName != null)
                {
                    queryString.Append($"AND LastName = '{filtering.LastName}' ");
                }
                if (filtering.Pid != null)
                {
                    queryString.Append($"AND Pid = '{filtering.Pid}' ");
                }
                if (filtering.RoomNumber != null)
                {
                    queryString.Append($"AND Room.Number = '{filtering.RoomNumber}' ");
                }
                if(filtering.IsActive != null)
                {
                    queryString.Append($"AND IsActive='{filtering.IsActive}'");
                }
                if (filtering.StreetName != null)
                {
                    queryString.Append($"AND Address.StreetName='{filtering.StreetName}'");
                }
                if (filtering.CityName != null)
                {
                    queryString.Append($"AND PostalOffice.CityName='{filtering.CityName}'");
                }
                if (filtering.CountryName != null)
                {
                    queryString.Append($"AND Country.Name='{filtering.CountryName}'");
                }

            }

            if (sorting != null)
            {
                queryString.Append($"ORDER BY {sorting.SortBy} {sorting.SortOrder} ");
            }

            if ((paging.PageNumber > 0 && paging.PageSize > 0))
            {
                queryString.Append($"OFFSET ({paging.PageNumber} - 1) * {paging.PageSize} ROWS FETCH NEXT {paging.PageSize} ROWS ONLY ");
            }


            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
            using(connection)
            {
                using(SqlCommand command = new SqlCommand(queryString.ToString(), connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    List<IActiveGuestModel> activeGuests = new List<IActiveGuestModel>();
                    while(await reader.ReadAsync())
                    {
                        ActiveGuestModel activeGuest = new ActiveGuestModel();
                        activeGuest.Id = reader.GetGuid(0);
                        activeGuest.FirstName = reader.GetString(1);
                        activeGuest.LastName = reader.GetString(2);
                        activeGuest.Pid = reader.GetString(3);
                        activeGuest.IsActive=reader.GetBoolean(4);
                        activeGuest.RoomNumber = reader.GetInt32(5);
                        activeGuest.StreetName = reader.GetString(6);
                        activeGuest.CityName = reader.GetString(7);
                        activeGuest.CountryName = reader.GetString(8);
                        activeGuests.Add(activeGuest);
                    }
                    return activeGuests;
                }
            }
        }
        
        public async Task ChangeActivityStatusAsync(Guid id)
        {

            SqlCommand commandRead = new SqlCommand($"SELECT * FROM Guest WHERE Id = '{id}'", connection);
            await connection.OpenAsync();
            SqlDataReader reader = commandRead.ExecuteReader();
            GuestModel guest = new GuestModel();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    guest.Id = (Guid)reader["Id"];
                    guest.RoomId = (Guid)reader["RoomId"];
                }
            }
            reader.Close();

            using (connection)
            {
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command1 = new SqlCommand($"UPDATE Guest SET IsActive = 0 WHERE Id = '{id}';", connection, transaction);
                    command1.ExecuteNonQuery();

                    SqlCommand command2 = new SqlCommand($"UPDATE Reservation SET IsActive = 0 WHERE GuestId = '{id}';", connection, transaction);
                    command2.ExecuteNonQuery();


                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }

            }


        }

        public async Task ChangeActivityStatusCheckAsync(List<ActiveGuestModel> activeGuests)
        {
            foreach (ActiveGuestModel activeGuest in activeGuests)
            {
                if(activeGuest.select==true)
                { 
                SqlCommand commandRead = new SqlCommand($"SELECT * FROM Guest WHERE Id = '{activeGuest.Id}';", connection);
                await connection.OpenAsync();
                SqlDataReader reader = commandRead.ExecuteReader();
                GuestModel guest = new GuestModel();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        guest.Id = (Guid)reader["Id"];
                        guest.RoomId = (Guid)reader["RoomId"];
                    }
                }
                reader.Close();
                

                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            SqlCommand command1 = new SqlCommand($"UPDATE Guest SET IsActive = 0 WHERE Id = '{activeGuest.Id}';", connection, transaction);
                            command1.ExecuteNonQuery();

                            SqlCommand command2 = new SqlCommand($"UPDATE Reservation SET IsActive = 0 WHERE GuestId = '{activeGuest.Id}';", connection, transaction);
                            command2.ExecuteNonQuery();


                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                        }

                    connection.Close();

                }
            }


        }




    }
}
