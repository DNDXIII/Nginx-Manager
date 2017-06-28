using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/blacklists")]
    public class BlacklistController : AbstractController<Blacklist>
    {
        public BlacklistController(BlacklistDataAccess repository) : base(repository) { }
    }
}
