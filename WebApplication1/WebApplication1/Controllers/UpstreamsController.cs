using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    [Route("api/upstreams")]
    public class UpstreamsController : AbstractController<Upstream>
    {
        public UpstreamsController(UpstreamDataAccess upstreamRepository):base(upstreamRepository){ }
    }
}
