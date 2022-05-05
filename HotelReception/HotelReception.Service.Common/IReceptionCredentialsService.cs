using HotelReception.Model;
using HotelReception.Model.Common;
using System.Threading.Tasks;

namespace HotelReception.Service.Common
{
    public interface IReceptionCredentialsService
    {
        Task<IReceptionistModel> GetLoginCredentials(IReceptionCredentialsModel credentials);
    }
}