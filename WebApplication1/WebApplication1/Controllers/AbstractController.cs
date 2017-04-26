﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    public class AbstractController<E> : Controller
    {
        private readonly IRepository<E> _repository;

        public AbstractController(IRepository<E> repository)
        {
            _repository = repository;
        }

        //since i couldnt find a way to make a specific route using only the query string
        // GET: api/values || Get: api/servers?_sort=title&_order=sdf&_start=1&_end=2
        [HttpGet]
        public IActionResult Get([FromQuery] string _sort, [FromQuery] string _order = "ASC", [FromQuery] int _start = 0, [FromQuery] int _end = 24)
        {
            if (_sort == null) { 
           
                return Ok(_repository.GetAll());
            }
            else
            {
                var es = _repository.GetList(_sort, _order, _start, _end);

                if (es == null || es.Count() == 0)
                    return NotFound();

                return Ok(es);
            }

        }
       
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var e = _repository.GetById(id);
            if (e == null)
                return NotFound();

            return Ok(e);
        }
       
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]E value)
        {
            if (value == null)
                return BadRequest();

            var createdE = _repository.Add(value);

            return Ok(createdE);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]E value)
        {
            if (value == null)
                return BadRequest();

            var note = _repository.GetById(id);

            if (note == null)
                return NotFound();

           

            _repository.Update(id, value);

            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        { 
            var e = _repository.GetById(id);
            if (e == null)
                return NotFound();

            _repository.Delete(id);

            return new OkResult();
        }
    }
}