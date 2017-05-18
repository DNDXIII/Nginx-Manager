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

        [EnableCors("Cors")]
        [HttpGet]
        public void Get()
        {
            Response.ContentType = "text/plain";
            Response.Headers.Append("Content-Description", "File Transfer");
            Response.Headers.Append("Content-Disposition", "attachment; filename=configuration.conf");
            Response.WriteAsync(_config.GenerateConfig(_allRep));
        }
    }
}