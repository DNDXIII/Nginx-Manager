using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class UserDataAccess : AbstractDataAccess<User>
    {
        public UserDataAccess(string connectionString) : base("Users", connectionString)
        {
        }
    }
}
