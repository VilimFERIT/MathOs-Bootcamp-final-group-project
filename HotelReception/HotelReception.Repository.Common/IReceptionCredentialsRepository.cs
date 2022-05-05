using HotelReception.Model;
using HotelReception.Model.Common;
using System.Threading.Tasks;

namespace HotelReception.Repository.Common
{
    public interface IReceptionCredentialsRepository
    {
        Task<IReceptionistModel> GetLoginCredentials(IReceptionCredentialsModel credentials);

    }
}