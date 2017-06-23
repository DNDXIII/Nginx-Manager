using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    [Route("api/virtualservers")]
    public class VirtualServerController : AbstractController<VirtualServer>
    {
        public VirtualServerController(IRepository<VirtualServer> repository) : base(repository)
        {
        }
    }
}
