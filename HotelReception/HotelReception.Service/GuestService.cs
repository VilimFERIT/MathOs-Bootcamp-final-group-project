using HotelReception.Common.GetParameters;
using HotelReception.Common.GetReservation;
using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository.Common;
using HotelReception.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Service
{
    public class GuestService : IGuestService
    {
        public IGuestRepository Repository { get; set; }

        public GuestService(IGuestRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<IGuestModel>> GetAllGuestsServiceAsync(Sorting sorting, Paging paging, GuestFiltering filtering)
        {
            return await Repository.GetAllGuestsAsync(sorting, paging, filtering);
        }

        public async Task<List<IActiveGuestModel>> GetActiveGuests(Sorting sorting, Paging paging, ActiveGuestFiltering filtering)
        {
            return await Repository.GetActiveGuests(sorting,paging,filtering);
        }
        public async Task InsertNewGuestAsync(InsertGuestDomain insertGuest)
        {
            GuestModel newGuest = new GuestModel
            {
                Id = insertGuest.GuestIdPrimary,
                Pid = insertGuest.Pid,
                FirstName = insertGuest.FirstName,
                LastName = insertGuest.LastName,
                AddressId = insertGuest.AddressId,
                IsActive = insertGuest.IsActive
            };

            CountryModel country = new CountryModel
            {
                Id = insertGuest.CountryIdPrimary,
                Name = insertGuest.Name
            };

            AddressModel address = new AddressModel
            {
                Id = insertGuest.AddressIdPrimary,
                StreetName = insertGuest.StreetName,
                PostalOfficeId = insertGuest.PostalOfficeId,

            };

            PostalOfficeModel postalOffice = new PostalOfficeModel
            {
                Id = insertGuest.PostalOfficeIdPrimary,
                Number = insertGuest.PostalOfficeNumber,
                CityName = insertGuest.CityName,
                CountryId = insertGuest.CountryId
            };

            RoomModel room = new RoomModel
            {
                Number = insertGuest.RoomNumber,
            };

            await Repository.InsertNewGuestAsync(newGuest, country, address, postalOffice, room);
        }

        public async Task ChangeActivityStatus(Guid id)
        {
            await Repository.ChangeActivityStatusAsync(id);
        }

        public async Task ChangeActivityStatusCheckAsync(List<ActiveGuestModel> activeGuests)
        {
            await Repository.ChangeActivityStatusCheckAsync(activeGuests);
        }
    }
}
