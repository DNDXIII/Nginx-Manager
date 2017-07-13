using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/whitelists")]
    public class BlacklistController : AbstractController<Whitelist>
    {
        public BlacklistController(WhitelistDataAccess repository) : base(repository) { }


    }
}
