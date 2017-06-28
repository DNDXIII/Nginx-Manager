using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/applications")]
    public class ApplicationController : AbstractController<Application>
    {
        public ApplicationController(ApplicationDataAccess repository) : base(repository) { }
    }
}
