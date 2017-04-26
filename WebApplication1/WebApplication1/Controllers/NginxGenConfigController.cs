using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/generateconfig")]
    public class NginxGenConfigController : Controller
    {
        public NginxGenConfigController(xxxx)
        {

        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok();//TODO
        }
    }
}