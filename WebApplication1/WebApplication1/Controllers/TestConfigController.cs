using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;

namespace WebApplication1.Controllers
{
    [Route("api/testconfig")]
    public class TestConfigController : Controller
    {
        private readonly AllRepositories _allRep;
        public TestConfigController(AllRepositories allRep)
        {
            _allRep = allRep;
        }

        [EnableCors("Cors")]
        [HttpGet]
        public IActionResult Get()
        {


            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = "cmd.exe";
            pi.Arguments = "/K echo hey";
            p.StartInfo = pi;
            p.Start();
            p.WaitForExit();




            return Ok();
        }

    }
}