using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository.Common;

namespace HotelReception.Repository
{
    public class ReceiptRepository : IReceiptRepository
    {
        //static string connectionString = @"Data Source=ST-02\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString"));

        public async Task<List<IReceipt>> GetReceiptsAsync(Sorting sorting, Paging paging, ReceiptFiltering receiptFiltering)
        {
            StringBuilder queryString = new StringBuilder($"SELECT * FROM Receipt ");

            if (receiptFiltering != null)
            {
                queryString.Append($"WHERE 1=1 ");
                
                if (receiptFiltering.Id != null)
                {
                    queryString.Append($"AND Id = {receiptFiltering.Id} ");
                }
                if (receiptFiltering.Price != null)
                {
                    queryString.Append($"AND Price = {receiptFiltering.Price} ");
                }
                if (receiptFiltering.PaymentMethod != null)
                {
                    queryString.Append($"AND PaymentMethod = {receiptFiltering.PaymentMethod} ");
                }
                if (receiptFiltering.ReservationId != null)
                {
                    queryString.Append($"AND ReservationId = {receiptFiltering.ReservationId} ");
                }
            }

            if (sorting.SortBy != null)
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
                List<IReceipt> listOfReceipts = new List<IReceipt>();

                while (await reader.ReadAsync())
                {
                    Receipt retrievedReceipt = new Receipt
                    {
                        Id = (Guid)reader["Id"],
                        Price = (decimal)reader["Price"],
                        PaymentMethod = (Guid)reader["PaymentMethod"],
                        ReservationId = (Guid)reader["ReservationId"]
                    };

                    listOfReceipts.Add(retrievedReceipt);
                }
                connection.Close();
                return listOfReceipts;
            }
            else
            {
                return null;
            }
        }

        

        public async Task PostReceiptAsync(Receipt receipt)
        {
            SqlCommand command = new SqlCommand
                ($"INSERT INTO Receipt (Price, PaymentMethod, ReservationID) VALUES ({receipt.Price}, '{receipt.PaymentMethod}', {receipt.ReservationId});", connection);

            await connection.OpenAsync();

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = command;
            await adapter.InsertCommand.ExecuteNonQueryAsync();

            connection.Close();
        }
    }
}
