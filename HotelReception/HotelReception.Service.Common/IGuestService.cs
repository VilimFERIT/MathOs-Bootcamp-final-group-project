using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Service.Common
{
    public interface IGuestService
    {
        Task<List<IGuestModel>> GetAllGuestsServiceAsync(Sorting sorting, Paging paging, GuestFiltering filtering);

        Task InsertNewGuestAsync(InsertGuestDomain insertGuest);

        Task<List<IActiveGuestModel>> GetActiveGuests(Sorting sorting, Paging paging, ActiveGuestFiltering filtering);

        Task ChangeActivityStatus(Guid pid);

        Task ChangeActivityStatusCheckAsync(List<ActiveGuestModel> activeGuests);
    }
}
