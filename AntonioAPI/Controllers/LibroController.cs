using AntonioAPI.Data;
using AntonioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AntonioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        public Contexto _contexto { get; set; }
        public BibliotecaRepositorio Repo { get; set; }
        public LibroController(Contexto contexto, BibliotecaRepositorio repo)
        {
            _contexto = contexto;
            Repo = repo;
        }

        // GET: api/<LibroController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> Get()
        {
            return Ok(await _contexto.Libros.ToListAsync());
        }

        // GET api/<LibroController>/5
        [HttpGet("{autorID}")]
        public async Task<ActionResult<List<Libro>>> Get(int autorID)
        {
            var libros = await _contexto.AutorLibro
                .Where(al => al.autorId == autorID)
                .Select(al => al.libro)
                .ToListAsync();

            if (libros == null || !libros.Any())
            {
                return NotFound();
            }

            return Ok(libros);
        }

        // POST api/<LibroController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Libro libro)
        {
            if (libro == null)
            {
                return BadRequest();
            }

            await _contexto.Libros.AddAsync(libro);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = libro.id }, libro);
        }

        // PUT api/<LibroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<LibroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
