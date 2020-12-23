using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain.Models;
using ProAgil.Repository.Interface;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;    
        private readonly IMapper _mapper;    
        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _mapper = mapper;    
            _repo = repo;   
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            { 
                var eventos = await _repo.GetAllEventoAsync(true);
                var results = _mapper.Map<EventoDto[]>(eventos);
                return  Ok(results);   
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha Conexão Banco Dados. Erro:" + ex.Message);
            }    
           
        }




        [HttpPost("Upload")]
        public IActionResult Upload()
        {
            try
            { 
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources","Imagens");   
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),folderName); 

                if (file.Length > 0)
                {
                    var fileName =  ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\""," ").Trim());

                    using(var stream = new FileStream(fullPath,FileMode.Create)){
                        file.CopyTo(stream);
                    }
                } 
                return Ok();
                    
                   
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha Conexão Banco Dados. Erro:" + ex.Message);
            }  

            
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            { 
                var evento = await _repo.GetAllEventoAsyncById(EventoId, true);
                var results = _mapper.Map<EventoDto>(evento);
                return  Ok(results);   
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha Conexão Banco Dados. Erro:" + ex.Message);
            }    
           
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            { 
                var eventos =await _repo.GetAllEventoAsyncByTema(tema, true);
                var results = _mapper.Map<EventoDto[]>(eventos);
                return  Ok(results);   
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha Conexão Banco Dados. Erro:" + ex.Message);
            }    
           
        }

       [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            { 
                var evento = _mapper.Map<Evento>(model);
               _repo.Add(evento);

               if (await _repo.SaveChangesAsync()){
                   return Created($"/api/evento{model.Id}", _mapper.Map<EventoDto>(evento));
               }
            }
             catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha Conexão Banco Dados. Erro:" + ex.Message);
            }      
           
            return BadRequest();
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, EventoDto model)
        {
            try
            {   
               var evento = await _repo.GetAllEventoAsyncById(EventoId, false);
               if (evento == null) return NotFound();

               _mapper.Map(model, evento);
               _repo.Update(evento);

               if (await _repo.SaveChangesAsync()){
                   return Created($"/api/evento{model.Id}", _mapper.Map<EventoDto>(evento));
               }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha Conexão Banco Dados. Erro:" + ex.Message);
            }      
           
            return BadRequest();
        }


        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {   
                var evento = await _repo.GetAllEventoAsyncById(EventoId, false);
                if (evento == null) return NotFound();

               _repo.Delete(evento);

               if (await _repo.SaveChangesAsync()){
                   return Ok();
               }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha Conexão Banco Dados. Erro:" + ex.Message);
            }     
           
            return BadRequest();
        }

        
    }
}