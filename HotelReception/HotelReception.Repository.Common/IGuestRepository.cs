using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;

namespace HotelReception.Repository.Common
{
    public interface IGuestRepository
    {
        Task<List<IGuestModel>> GetAllGuestsAsync(Sorting sorting, Paging paging, GuestFiltering receiptFiltering);

        Task InsertNewGuestAsync(IGuestModel newGuest, ICountryModel country, IAddressModel address, IPostalOfficeModel postalOffice, RoomModel room);

        Task ChangeActivityStatusAsync(Guid id);

        Task<List<IActiveGuestModel>> GetActiveGuests(Sorting sorting, Paging paging, ActiveGuestFiltering filtering);

        Task ChangeActivityStatusCheckAsync(List<ActiveGuestModel> activeGuests);
    }
}
