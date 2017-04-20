using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/upstreams")]
    public class UpstreamsController : AbstractController<Upstream>
    {
        public UpstreamsController(IRepository<Upstream> upstreamRepository):base(upstreamRepository){ }
    }
}
