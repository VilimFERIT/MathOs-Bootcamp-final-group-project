using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository.Common;

namespace HotelReception.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        //static string connectionString = @"Data Source=MARIN\SQLEXPRESS01;Initial Catalog=HotelDatabase;Integrated Security=True";
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));


        public async Task<List<IReservation>> GetReservationsAsync(Sorting sorting, Paging paging, ReservationFiltering filtering)
        {
            

            // Default values --> SortBy = Id, SortOrder = asc, PageNumber = 1, PageSize = 10, Filtering = null
            StringBuilder queryString = new StringBuilder($"SELECT Guest.FirstName, Guest.LastName, Reservation.CreationDate, Reservation.IsActive FROM Reservation INNER JOIN Guest ON Guest.Id = Reservation.GuestId ");



            if (filtering != null)
                {
                queryString.Append($"WHERE 1=1 ");

                if (filtering.CreationDate != null)
                {
                    queryString.Append($"AND CreationDate = {filtering.CreationDate} ");
                }
                if (filtering.IsActive != null)
                {
                    queryString.Append($"AND Reservation.IsActive = {filtering.IsActive} ");
                }
            }


            SqlCommand command = new SqlCommand(queryString.ToString(), connection);

            await connection.OpenAsync();
            
            SqlDataReader reader = await command.ExecuteReaderAsync();
            
            if (reader.HasRows)
            {
                List<IReservation> listOfReservations = new List<IReservation>();

                while (await reader.ReadAsync())
                {
                    Reservation retrievedReservation = new Reservation();
                    GuestModel retrievedGuests = new GuestModel();

                    retrievedReservation.CreationDate = (DateTime)reader["CreationDate"];
                    retrievedReservation.IsActive = (bool)reader["IsActive"];
                    retrievedGuests.FirstName = (string)reader["FirstName"];
                    retrievedGuests.LastName = (string)reader["LastName"];

                    retrievedReservation.guest = retrievedGuests;


                    listOfReservations.Add(retrievedReservation);
                }
                connection.Close();
                return listOfReservations;
            }
            else
            {
                connection.Close();
                return null;
            }
         
        }


        public async Task PostReservationAsync(Receipt receipt, Reservation reservation, ReceptionistModel receptionist, RoomReservation roomReservation, Payment payment, string pid)
        {
            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));


            // Retrieve Receptionist (Id)
            SqlCommand commandRead = new SqlCommand($"SELECT * FROM Receptionist WHERE FirstName = '{receptionist.FirstName}' AND LastName = '{receptionist.LastName}';", connection);
            await connection.OpenAsync(); 
            SqlDataReader reader = await commandRead.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    receptionist.Id = (Guid)reader["Id"];
                }
                
            }
            reader.Close();

            // Retrieve Guest (Id, RoomId)
            SqlCommand commandRead2 = new SqlCommand($"SELECT * FROM Guest WHERE Pid = '{pid}'", connection);
            SqlDataReader reader2 = await commandRead2.ExecuteReaderAsync();
            GuestModel retrievedGuest = new GuestModel();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    retrievedGuest.Id = (Guid)reader2["Id"];
                    retrievedGuest.RoomId = (Guid)reader2["RoomId"];
                }
            }
            reader2.Close();

            // Retrieve Room (Price)
            SqlCommand commandRead3 = new SqlCommand($"SELECT * FROM Room WHERE Id = '{retrievedGuest.RoomId}';", connection);
            SqlDataReader reader3 = await commandRead3.ExecuteReaderAsync();
            RoomModel retrievedRoom = new RoomModel();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    retrievedRoom.Price = (Decimal)reader3["Price"];
                }
            }
            reader3.Close();

            
            SqlCommand commandGetDays = new SqlCommand($"SELECT DATEDIFF(day, {roomReservation.EndDate.ToString("yyyy-MM-dd")}, {roomReservation.StartDate.ToString("yyyy-MM-dd")});", connection);
            SqlDataReader reader4 = await commandGetDays.ExecuteReaderAsync(); 
            int numberOfDays = 0;
            if (reader4.HasRows)
            {
                while (reader4.Read())
                {
                    numberOfDays = reader4.GetInt32(0);
                }
            }
            reader4.Close();

            SqlCommand commandRead5 = new SqlCommand($"SELECT * FROM Payment WHERE Method = '{payment.Method}';", connection);
            SqlDataReader reader5 = await commandRead5.ExecuteReaderAsync();
            Payment newPayment = new Payment();
            if (reader5.HasRows)
            {
                while (reader5.Read())
                {
                    newPayment.Id= (Guid)reader5["Id"];
                }
            }
            reader5.Close();

            // Insert info into three tables
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {

                SqlCommand commandInsert1 = new SqlCommand($"INSERT INTO Reservation VALUES ('{reservation.Id}', '{receptionist.Id}', '{reservation.CreationDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 1, '{retrievedGuest.Id}');", connection, transaction);
                commandInsert1.ExecuteNonQuery();
                
                
                SqlCommand commandInsert2 = new SqlCommand($"INSERT INTO Receipt VALUES ('{receipt.Id}', @Price, '{newPayment.Id}', '{reservation.Id}');", connection, transaction);
                commandInsert2.Parameters.Add("@Price", System.Data.SqlDbType.Decimal);
                commandInsert2.Parameters["@Price"].Value = retrievedRoom.Price * numberOfDays;
                commandInsert2.ExecuteNonQuery();


                SqlCommand commandInsert3 = new SqlCommand
                    ($"INSERT INTO RoomReservation VALUES ('{retrievedGuest.RoomId}', '{reservation.Id}', '{roomReservation.StartDate.ToString("yyyy-MM-dd")}', '{roomReservation.EndDate.ToString("yyyy-MM-dd")}', 1)", connection, transaction);
                commandInsert3.ExecuteNonQuery();
              
                   
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                throw new Exception($"Errr", ex);
                
            }
            
            connection.Close();
        }


        public async Task PutReservationAsync(Guid id)
        {
            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));

            SqlTransaction transaction;

            SqlCommand commandRead = new SqlCommand($"SELECT * FROM Reservation WHERE Id = '{id}' ", connection);
            await connection.OpenAsync();

            SqlDataReader reader = await commandRead.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                reader.Close();
                transaction = connection.BeginTransaction();
                try
                {
                    new SqlCommand($"UPDATE Reservation SET IsActive = 1 WHERE Id = '{id}';", connection, transaction).ExecuteNonQuery();

                    // Update Guest - set inactive
                    transaction.Commit();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }

                connection.Close();
            }
        }
    }
}
