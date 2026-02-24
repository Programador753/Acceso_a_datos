using AntonioAPI.Data;
using AntonioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("autores-por-premio/{premioId}")]
        public async Task<ActionResult<IEnumerable<AutorConAnio>>> GetAutoresPorPremio(int premioId)
        {
            var resultados = await _contexto.AutorPremios
                .Include(ap => ap.autor) 
                .Where(ap => ap.premioId == premioId)
                .Select(ap => new AutorConAnio
                {
                    anio = ap.anio,
                    autor = ap.autor
                })
                .ToListAsync();

            if (resultados == null || resultados.Count == 0)
            {
                return NotFound(new { mensaje = "No se encontraron autores con este premio." });
            }

            return Ok(resultados);
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
            // El repo devuelve una Lista, así que extraemos el primero
            var primerAutor = autor.FirstOrDefault();

            if (primerAutor == null)
            {
                return NotFound();
            }
            return Ok(primerAutor);
        }

        // POST api/<AutorController>
        [HttpPost]
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
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            return BadRequest("El método PUT no está implementado");
        }

        // DELETE api/<AutorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // NOTA: Para que el DELETE funcione necesitarás crear el método DeleteAutorAsync en tu Repositorio
            var autor = await _contexto.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            _contexto.Autores.Remove(autor);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}