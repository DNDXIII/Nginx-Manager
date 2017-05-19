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
            //creates the text file
            StringBuilder config = new StringBuilder(_allRep.GeneralConfigRep.GetAll().First().GenerateConfig(_allRep));

            foreach (var sv in _allRep.ServerRep.GetAll())
                config.Replace(sv.Address, "127.0.0.1");

            System.IO.File.WriteAllText(@"D:\nb23160\Downloads\configuration.conf", config.ToString());



            //tests the file and writes the output 
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = "cmd.exe";
            pi.Arguments = @"/C docker run -v D:\nb23160\Downloads\configuration.conf:/etc/nginx/nginx.conf -P -it imkulikov/nginx-sticky nginx -t";
            pi.RedirectStandardOutput = true;
            p.StartInfo = pi;
            p.Start();

            var s  = p.StandardOutput.ReadToEnd();

            p.WaitForExit();
            
            //check if it was a positive response
            if(s.Contains("failed"))
                return BadRequest(s);
            return Ok(s);
        }

    }
}