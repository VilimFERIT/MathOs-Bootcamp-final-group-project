using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Repository.Common;

namespace HotelReception.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        //static string connectionString = @"Data Source=MARIN\SQLEXPRESS01;Initial Catalog=HotelDatabase;Integrated Security=True";
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));

        public async Task<List<Payment>> GetPaymentsAsync(Sorting sorting, Paging paging, PaymentFiltering paymentFiltering)
        {
            

            string queryString = $"SELECT * FROM Payment";

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();

            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                List<Payment> listOfPayments = new List<Payment>();

                while (await reader.ReadAsync())
                {
                    Payment payment = new Payment
                    {
                        Id = (Guid)reader["Id"],
                        Method = (string)reader["Method"]
                    };

                    listOfPayments.Add(payment);
                }
                connection.Close();
                return listOfPayments;
            }
            else
            {
                connection.Close();
                return null;
            }
        }
    }
}
