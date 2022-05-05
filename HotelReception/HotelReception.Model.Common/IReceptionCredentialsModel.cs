using System;
using System.Data.SqlTypes;

namespace HotelReception.Model.Common
{
    public interface IReceptionCredentialsModel
    {
        Guid ID { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}