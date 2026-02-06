using ColegioAPI.Data;
using ColegioAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        public CalificacionRepositorio Repositorio { get; }
        public CalificacionController(CalificacionRepositorio repositorio)
        {
            Repositorio = repositorio;
        }

        // GET: api/<CalificacionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calificacion>>> Get()
        {
            return await Repositorio.ObtenerCalificacionesAsync();
        }

        // GET api/<CalificacionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CalificacionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CalificacionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CalificacionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
