using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class LocationsDataAccess: AbstractDataAccess<Location>
    {
        public LocationsDataAccess(string connectionString) : base("Locations", connectionString) { }
    }
}
