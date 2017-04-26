using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
