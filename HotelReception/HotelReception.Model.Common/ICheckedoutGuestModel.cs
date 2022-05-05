using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model.Common
{
    public interface ICheckedoutGuestModel
    {
         Guid Id { get; set; }

        bool IsActive { get; set; }
    }
}
