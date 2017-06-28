using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class BlacklistDataAccess : AbstractDataAccess<Blacklist>
    {
        public BlacklistDataAccess(string connectionString) : base("Blacklist", connectionString)
        {
        }
    }
}
