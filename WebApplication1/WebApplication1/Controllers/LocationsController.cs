using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/locations")]
    public class LocationsController : AbstractController<Location>
    {
        public LocationsController(IRepository<Location> repository) : base(repository) { }
    }
}
