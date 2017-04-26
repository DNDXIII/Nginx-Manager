using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    [Route("api/generateconfig")]
    public class NginxGenConfigController : Controller
    {
        private readonly AllRepositories _allRep;
        private readonly NGINXConfig _config;
        public NginxGenConfigController(AllRepositories allRep)
        {
            _allRep = allRep;
            _config = new NGINXConfig();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_config.GenerateConfig(_allRep));
        }
    }
}