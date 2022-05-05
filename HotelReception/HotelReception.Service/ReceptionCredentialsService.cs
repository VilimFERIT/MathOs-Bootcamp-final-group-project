using HotelReception.Model;
using HotelReception.Model.Common;
using HotelReception.Repository;
using HotelReception.Repository.Common;
using HotelReception.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Service
{
    public class ReceptionCredentialsService : IReceptionCredentialsService
    {
        protected IReceptionCredentialsRepository Repository { get; set; }
        public ReceptionCredentialsService(IReceptionCredentialsRepository repository)
        {
            Repository = repository;
        }

        public async Task<IReceptionistModel> GetLoginCredentials(IReceptionCredentialsModel credentials)
        {
            return await Repository.GetLoginCredentials(credentials);
        }



    }
}
