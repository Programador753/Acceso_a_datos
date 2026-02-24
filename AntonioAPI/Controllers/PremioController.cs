using AntonioAPI.Data;
using AntonioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AntonioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremioController : ControllerBase
    {

        public Contexto _contexto { get; set; }
        public BibliotecaRepositorio Repo { get; set; }
        public PremioController(Contexto contexto, BibliotecaRepositorio repo)
        {
            _contexto = contexto;
            Repo = repo;
        }

        // GET: api/<PremioController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Premio>>> GetPremios()
        {
            // _contexto.Premio o _contexto.Premios dependiendo de cómo esté en tu DbContext
            var premios = await _contexto.Premios.ToListAsync();
            return Ok(premios);
        }

        // GET: api/<PremioController>
        [HttpGet("autor")]
        public async Task<ActionResult<IEnumerable<Premio>>> GetPremiosTipoAutor()
        {
            // _contexto.Premio o _contexto.Premios dependiendo de cómo esté en tu DbContext
            var premios = await _contexto.Premios.Where(p => p.tipo == "A").ToListAsync();
            return Ok(premios);
        }

        // GET api/<PremioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PremioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PremioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PremioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
