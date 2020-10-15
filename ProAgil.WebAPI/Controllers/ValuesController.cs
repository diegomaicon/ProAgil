﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain.Models;
using ProAgil.Repository.Data;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly ProAgilDataContext _context;

        public ValuesController(ProAgilDataContext context )
        {
          _context = context;  
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            { 
                var results = await _context.Eventos.ToListAsync();
                return  Ok(results);   
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Fala Conexão Banco Dados");
            }    
           
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id)
        {  
            try
            { 
               var results = await _context.Eventos.FirstOrDefaultAsync(x => x.Id == id);
               return Ok(results);      
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Fala Conexão Banco Dados");
            } 
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
