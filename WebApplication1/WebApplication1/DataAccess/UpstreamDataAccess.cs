
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class UpstreamDataAccess : AbstractDataAccess<Upstream>
    {
        public UpstreamDataAccess(string connectionString) : base("Upstreams", connectionString) {
         }     
    }
}
