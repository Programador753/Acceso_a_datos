using AntonioAPI.Data;
using AntonioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AntonioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        public Contexto _contexto { get; set; }
        public BibliotecaRepositorio Repo { get; set; }
        public AutorController(Contexto contexto, BibliotecaRepositorio repo)
        {
            _contexto = contexto;
            Repo = repo;
        }

        // GET: api/<AutorController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> Get()
        {
            return Ok(await Repo.GetAutoresAsync());
        }

        // GET api/<AutorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await Repo.GetAtoresById(id);
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        // POST api/<AutorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Autor>> Post(Autor autor)
        {
            try
            {
                var nuevoAutor = await Repo.AddAutorAsync(autor);
                return Ok(nuevoAutor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AutorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AutorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
