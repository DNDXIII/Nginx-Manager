using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/deploymentservers")]
    public class DeploymentServersController : AbstractController<DeploymentServer>
    {
        public DeploymentServersController(IRepository<DeploymentServer> repository) : base(repository) { }
    }
}
