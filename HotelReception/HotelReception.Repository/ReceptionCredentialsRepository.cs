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
    public class ReceptionCredentialsRepository : IReceptionCredentialsRepository
    {
        //protected static string connectionString = @"Data Source=ST-03;Initial Catalog=Test;Integrated Security=True";
       
        public async Task<IReceptionistModel> GetLoginCredentials(IReceptionCredentialsModel credentials)
        {
            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
            using (connection)
            {

                SqlCommand command1 = new SqlCommand
                ($"Select * FROM EndUser WHERE Username ='{credentials.Username}' AND Password = '{credentials.Password}' ", connection);
                SqlCommand command2 = new SqlCommand
                ($"Select * FROM Receptionist LEFT JOIN EndUser ON Receptionist.EndUserId = EndUser.Id", connection);
                await connection.OpenAsync();
                SqlDataReader reader1 = await command1.ExecuteReaderAsync();
                
                if (reader1.HasRows)
                {
                    await reader1.ReadAsync();
                    reader1.Close();

                    SqlDataReader reader2 = await command2.ExecuteReaderAsync();
                    await reader2.ReadAsync();
                    IReceptionistModel receptionist = new ReceptionistModel();
                    receptionist.Id = reader2.GetGuid(0);
                    receptionist.FirstName = reader2.GetString(1);
                    receptionist.LastName = reader2.GetString(2);
                
                    reader2.Close();
                    
                    connection.Close();
                    return receptionist;
                }
                else
                {
                    connection.Close();
                    return null;
                }


            }

        }
    }
    
}