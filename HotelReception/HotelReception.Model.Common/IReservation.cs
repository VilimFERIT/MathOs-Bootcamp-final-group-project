using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReception.Model;

namespace HotelReception.Model.Common
{
    public interface IReservation
    {
        Guid Id { get; set; }
        Guid ReceptionistId { get; set; }
        DateTime CreationDate { get; set; }
        bool IsActive { get; set; }
        IGuestModel guest { get; set; }
    }
}
