using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class ApplicationDataAccess :AbstractDataAccess<Application>
    {   
        public ApplicationDataAccess(string connectionString) : base("Applications", connectionString)
        { 
        }
    }
}
