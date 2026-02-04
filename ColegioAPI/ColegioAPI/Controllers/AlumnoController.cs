using ColegioAPI.Data;
using ColegioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        public MyContext Context { get; }
        public AlumnoController(MyContext context)
        {
            Context = context;
        }

        // GET: api/<AlumnoController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> Get()
        {
            return await Context.Alumnos.Include("Pais").ToListAsync();
        }

        // GET api/<AlumnoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> Get(int id)
        {
            return Context.Alumnos.Find(id);
        }

        // GET api/<AlumnoController>/5
        [HttpGet("busqueda/{busca}")]
        public async Task<ActionResult<IEnumerable<Alumno>>> Get(string busca)
        {
            return await Context.Alumnos.Where(a => a.Nombre.Contains(busca)).ToListAsync();
        }

        // GET api/<AlumnoController>/5
        [HttpGet("porPais/{id}")]
        public async Task<ActionResult<IEnumerable<Alumno>>> porPais(int id)
        {
            return await Context.Alumnos.Where(a => a.PaisID == id).ToListAsync();
        }

        // POST api/<AlumnoController>
        [HttpPost]
        public async Task<ActionResult<Alumno>> Post([FromBody] Alumno alumno)
        {
            if (alumno.Id != 0)
            {
                var alumnoActualizado = Context.Alumnos.Attach(alumno);
                alumnoActualizado.State = EntityState.Modified;
            }
            else
            {
                Context.Alumnos.Add(alumno);

            }
            await Context.SaveChangesAsync();
            return alumno;
        }
        
        // PUT api/<AlumnoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlumnoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Context.Database.ExecuteSqlRawAsync("DELETE FROM Alumnos WHERE Id = {0}", id);
            return Ok(new { mensaje = $"Alumno {id} eliminado" });
        }
    }
}
