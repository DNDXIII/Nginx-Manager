using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;
using Renci.SshNet;
using System;

namespace WebApplication1.Controllers
{
    [Route("api/config")]
    public class ConfigController : Controller
    {   
        private readonly AllRepositories _allRep;
        public ConfigController(AllRepositories allRep)
        {
            _allRep = allRep;
        }

        [EnableCors("Cors")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new NGINXConfig().GenerateConfig(_allRep));
        }

        [EnableCors("Cors")]
        [HttpGet("download")]
        public void Download()
        {
            var config = new NGINXConfig();
            Response.ContentType = "text/plain";
            Response.Headers.Append("Content-Description", "File Transfer");
            Response.Headers.Append("Content-Disposition", "attachment; filename=configuration.conf");
            Response.WriteAsync(config.GenerateConfig(_allRep));
        }


        [EnableCors("Cors")]
        [HttpGet("test")]
        public IActionResult TestConfig()
        {
            //creates the text file
            StringBuilder config = new StringBuilder(_allRep.GeneralConfigRep.GetAll().First().GenerateConfig(_allRep));

            foreach (var sv in _allRep.ServerRep.GetAll())
                config.Replace(sv.Address, "127.0.0.1");

            var filePath = Path.GetTempFileName();
            System.IO.File.WriteAllText(filePath, config.ToString());



            //tests the file and writes the output 
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = "cmd.exe";
            pi.Arguments = @"/C docker run -v " + filePath + @":/etc/nginx/nginx.conf -P -it nginx-image nginx -t";
            pi.RedirectStandardOutput = true;
            p.StartInfo = pi;
            p.Start();

            p.WaitForExit();
            var s = p.StandardOutput.ReadToEnd();

            Console.WriteLine(s);
            //check if it was a positive response
            if (p.ExitCode!=0)
                return StatusCode(500, s);
            return Ok(s);
        }

    }
}