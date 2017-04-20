using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/generateconfig")]
    public class NginxGenConfigController:Controller
    {
       // private readonly IRepository<Ups> _repository;
        public NginxGenConfigController() { }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            FileStream f = System.IO.File.Create(@"~/stuff/coise.txt");

            return File(f, f.GetType().ToString());
        }
    }
}
