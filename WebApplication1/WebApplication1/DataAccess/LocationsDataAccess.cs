using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class LocationsDataAccess: AbstractDataAccess<Location>
    {
        public LocationsDataAccess(string connectionString) : base("Locations", connectionString) { }
    }
}
