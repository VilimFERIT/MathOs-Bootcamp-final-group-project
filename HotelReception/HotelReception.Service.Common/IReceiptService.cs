using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;

namespace HotelReception.Service.Common
{
    public interface IReceiptService
    {
        Task<List<IReceipt>> GetReceiptsAsync(Sorting sorting, Paging paging, ReceiptFiltering receiptFiltering);

        Task PostReceiptAsync(Receipt receipt);

        
    }
}
