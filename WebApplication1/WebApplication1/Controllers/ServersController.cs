﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;


namespace WebApplication1.Controllers
{
    [Route("api/servers")]
    public class ServersController : AbstractController<Server>
    {
        public ServersController(IRepository<Server> serverRepository):base(serverRepository) { }
    }
}
