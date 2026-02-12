using ColegioAPI.Data;
using ColegioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturaController : ControllerBase
    {
        public MyContext Context { get; }
        public AsignaturaController(MyContext context)
        {
            Context = context;
        }

        // GET: api/<AsignaturaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignatura>>> Get()
        {
            return await Context.Asignaturas.ToListAsync();
        }

        // GET api/<AsignaturaController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            var asignatura = await Context.Asignaturas.FindAsync(id);
            return asignatura?.nomAsignatura;
        }

        // POST api/<AsignaturaController>
        [HttpPost]
        public async Task<ActionResult<Asignatura>> Post([FromBody] Asignatura asignatura)
        {
            if (asignatura.Id != 0)
            {
                var asignaturaActualizada = Context.Asignaturas.Attach(asignatura);
                asignaturaActualizada.State = EntityState.Modified;
            }
            else
            {
                Context.Asignaturas.Add(asignatura);
            }
            await Context.SaveChangesAsync();
            return asignatura;
        }

        // PUT api/<AsignaturaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AsignaturaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
