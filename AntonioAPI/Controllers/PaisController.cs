using System.Collections;
using AntonioAPI.Data;
using AntonioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AntonioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {

        public Contexto _contexto { get; set; }
        public BibliotecaRepositorio Repo { get; set; }
        public PaisController(Contexto contexto, BibliotecaRepositorio repo)
        {
            _contexto = contexto;
            Repo = repo;
        }

        // GET: api/<PaisController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> Get()
        {
            return Ok(await _contexto.Paises.ToListAsync());
        }
        
        // GET api/<PaisController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> Get(int id)
        {
            var pais = await _contexto.Paises.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }
            return Ok(pais);
        }

        // POST api/<PaisController>
        [HttpPost]
        public async Task<ActionResult<Pais>> Post([FromBody] Pais pais)
        {
            if (pais == null)
            {
                return BadRequest();
            }

            _contexto.Paises.Add(pais);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = pais.id}, pais);
        }

        // PUT api/<PaisController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PaisController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
