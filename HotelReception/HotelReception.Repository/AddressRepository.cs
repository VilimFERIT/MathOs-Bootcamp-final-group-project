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
    public class AddressRepository : IAddressRepository
    {
        //static string connectionString = @"Data Source=DESKTOP-KKL4FN6\SQLEXPRESS;Initial Catalog = vjezba2; Integrated Security = True";
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
        public async Task InsertNewAddressAsync(IAddressModel newAddress, IPostalOfficeModel postalOfficeNumber)
        {
            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (connection)
            {
                await connection.OpenAsync();
                string newAddressCommand = $"INSERT INTO Address (StreetName, PostalOfficeID) VALUES({newAddress.StreetName}, '{newAddress.PostalOfficeId}');";
                adapter.InsertCommand = new SqlCommand(newAddressCommand, connection);
                await adapter.InsertCommand.ExecuteNonQueryAsync();
            }

        }
    }
}
