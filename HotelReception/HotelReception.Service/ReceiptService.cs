using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository.Common;
using HotelReception.Service.Common;

namespace HotelReception.Service
{
    public class ReceiptService : IReceiptService
    {
        protected IReceiptRepository ReceiptRepository { get; set; }

        public ReceiptService(IReceiptRepository receiptRepository)
        {
            ReceiptRepository = receiptRepository;
        }


        public async Task<List<IReceipt>> GetReceiptsAsync(Sorting sorting, Paging paging, ReceiptFiltering receiptFiltering)
        {
            return await ReceiptRepository.GetReceiptsAsync(sorting, paging, receiptFiltering);
        }

        
        public async Task PostReceiptAsync(Receipt receipt)
        {
            await ReceiptRepository.PostReceiptAsync(receipt);
        }
    }
}
