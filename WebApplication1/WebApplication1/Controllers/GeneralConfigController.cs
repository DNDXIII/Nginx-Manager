using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/generalconfig")]
    public class GeneralConfigController : AbstractController<GeneralConfig>
    {
        public GeneralConfigController(IRepository<GeneralConfig> repository) : base(repository) { }
    }
}
