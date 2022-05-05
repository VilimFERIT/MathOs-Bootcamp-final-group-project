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
    public class CountryRepository : ICountryRepository
    {
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));
     
        
        public async Task<List<ICountryModel>> GetAllCountriesAsync(Sorting sorting, Paging paging, CountryFiltering filtering)
        {
            StringBuilder queryString = new StringBuilder("SELECT * FROM Country ");

            if (filtering != null)
            {
                queryString.Append("WHERE 1=1 ");

                if (filtering.Name != null)
                {
                    queryString.Append($"AND Name = '{filtering.Name}' ");
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
                    List<ICountryModel> countries = new List<ICountryModel>();
                    while (await reader.ReadAsync())
                    {
                        CountryModel country = new CountryModel();
                        country.Id = reader.GetGuid(0);
                        country.Name = reader.GetString(1);
                        countries.Add(country);
                    }
                    return countries;
                }


            }
        }
    
    
    }
}
