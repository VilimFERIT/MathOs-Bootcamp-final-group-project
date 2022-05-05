using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model.Common
{
    public interface IGuestModel
    {
        Guid Id { get; set; } 

        string Pid { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        Guid AddressId { get; set; }

        Guid RoomId { get; set; }

        IAddressModel Address { get; set; }

        bool IsActive { get; set; }
    }
}
