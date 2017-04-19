using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/upstreams")]
    public class UpstreamsController : Controller
    {
        private readonly IUpstreamsRepository _upstreamRepository;

        public UpstreamsController(IUpstreamsRepository upstreamRepository)
        {
            _upstreamRepository = upstreamRepository;
        }

        
        // GET: api/values
        [HttpGet]
        public IEnumerable<Upstream> Get()
        {
            return _upstreamRepository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetUpstream")]
        public IActionResult Get(int id)
        {
            var up = _upstreamRepository.GetById(id);
            if(up == null)
            {
                return NotFound();
            }
            return Ok(up);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Upstream value)
        {
            if (value == null)
                return BadRequest();

            var up = _upstreamRepository.GetById(value.RestId);

            if (up != null)
                return BadRequest();

            var createdUp = _upstreamRepository.Add(value);
            return CreatedAtRoute("GetUpstream", new { id = createdUp.Id }, createdUp);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Upstream value)
        {
            if (value == null)
                return BadRequest();

            var note = _upstreamRepository.GetById(id);

            if (note == null)
                return NotFound();

            if (note.RestId != value.RestId)
                return BadRequest();

            _upstreamRepository.Update(value);
            
            return NoContent();
        }

 

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var up = _upstreamRepository.GetById(id);
            if (up== null)
                return NotFound();

            _upstreamRepository.Delete(id);

            return new OkResult();
        }
    }
}
