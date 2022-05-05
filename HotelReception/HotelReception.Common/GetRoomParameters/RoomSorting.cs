using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Common.GetRoomParameters
{
    public class RoomSorting
    {
        protected string sortBy = "Number";

        public string SortBy
        {
            get
            {
                return sortBy;
            }
            set
            {
                sortBy = value;

            }
        }

        protected string sortOrder = "asc";

        public string SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                sortOrder = value;

            }
        }
    }
}
