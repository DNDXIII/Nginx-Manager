﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class ProxyTypeDataAccess: AbstractDataAccess<ProxyType>
    {
        public ProxyTypeDataAccess() : base("ProxyTypes") { }
    }
}
