using System;

namespace HotelReception.Model.Common
{
    public interface IReceptionistModel
    {
        string FirstName { get; set; }
        Guid Id { get; set; }
        string LastName { get; set; }
    }
}