using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/servers")]
    public class ServersController : Controller
    {
        private readonly IRepository<Server> _serverRepository;

        public ServersController(IRepository<Server> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        //since i couldnt find a way to make a specific route using only the query string
        // GET: api/values || Get: api/servers?_sort=title&_order=sdf&_start=1&_end=2
        [HttpGet]
        public IActionResult Get([FromQuery] string _sort, [FromQuery] string _order = "ASC", [FromQuery] int _start = 0, [FromQuery] int _end = 24)
        {
            if (_sort == null) { 
           
                return Ok(_serverRepository.GetAll());
            }
            else
            {
                var svrs = _serverRepository.GetList(_sort, _order, _start, _end);

                if (svrs == null || svrs.Count() == 0)
                    return NotFound();

                return Ok(svrs);
            }

        }
       
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var sv = _serverRepository.GetById(id);
            if (sv == null)
                return NotFound();

            return Ok(sv);
        }
       
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Server value)
        {
            if (value == null)
                return BadRequest();

            var createdSv = _serverRepository.Add(value);

            return Json(createdSv);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Server value)
        {
            if (value == null)
                return BadRequest();

            var note = _serverRepository.GetById(id);

            if (note == null)
                return NotFound();

           

            _serverRepository.Update(id, value);

            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        { 
            var sv = _serverRepository.GetById(id);
            if (sv == null)
                return NotFound();

            _serverRepository.Delete(id);

            return new OkResult();
        }
    }
}
