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
    public class PostalOfficeRepository : IPostalOfficeRepository
    {
        //static string connectionString = @"Data Source=DESKTOP-KKL4FN6\SQLEXPRESS;Initial Catalog = vjezba2; Integrated Security = True";
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
        public async Task InsertNewPostalOfficeAsync(ICountryModel country, IPostalOfficeModel newPostalOffice)
        {
            
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (connection)
            {
                await connection.OpenAsync();
                string newAddressCommand = $"INSERT INTO PostalOffice (Number, CityName, CountryID) VALUES({newPostalOffice.Number}, '{newPostalOffice.CityName}','{country.Id}');";
                adapter.InsertCommand = new SqlCommand(newAddressCommand, connection);
                await adapter.InsertCommand.ExecuteNonQueryAsync();
            }
        }
    }
}
