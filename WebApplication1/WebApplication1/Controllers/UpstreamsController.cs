using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    [Route("api/upstreams")]
    public class UpstreamsController : AbstractController<Upstream>
    {
        public UpstreamsController(IRepository<Upstream> upstreamRepository):base(upstreamRepository){ }
    }
}
