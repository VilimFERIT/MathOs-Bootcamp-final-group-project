﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReception.Model.Common
{
    public interface ICountryModel
    {
        Guid Id { get; set; }

        string Name { get; set; }
    }
}