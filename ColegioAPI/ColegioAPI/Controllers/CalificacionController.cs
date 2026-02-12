using ColegioAPI.Data;
using ColegioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        private readonly CalificacionRepositorio _repositorio;

        public CalificacionController(CalificacionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        // GET: api/Calificacion
        // Esta ruta coincide con 'this.apiUrl + /Calificacion' en Angular
        // Devuelve la lista CON detalles (Alumno y Asignatura)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calificacion>>> Get()
        {
            return await _repositorio.ObtenerCalificacionesAsync();
        }

        // GET: api/Calificacion/simple
        // Ruta extra por si necesitas la lista cruda (sin includes) para pruebas
        [HttpGet("simple")]
        public async Task<ActionResult<IEnumerable<Calificacion>>> GetAllSimple()
        {
            return await _repositorio.Context.Calificaciones.ToListAsync();
        }

        // GET: api/Calificacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calificacion>> Get(int id)
        {
            var calificacion = await _repositorio.Context.Calificaciones.FindAsync(id);
            if (calificacion == null) return NotFound();
            return calificacion;
        }

        // POST: api/Calificacion
        // Recibe los datos del formulario de Angular
        [HttpPost]
        public async Task<ActionResult<Calificacion>> Post([FromBody] Calificacion calificacion)
        {
            if (calificacion == null) return BadRequest("Datos inválidos");

            // Si el ID es distinto de 0, actualizamos; si no, creamos.
            if (calificacion.ID != 0)
            {
                // Entity Framework 'Update' maneja el estado 'Modified' automáticamente
                _repositorio.Context.Calificaciones.Update(calificacion);
            }
            else
            {
                _repositorio.Context.Calificaciones.Add(calificacion);
            }

            await _repositorio.Context.SaveChangesAsync();
            return Ok(calificacion);
        }

        // DELETE: api/Calificacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var calificacion = await _repositorio.Context.Calificaciones.FindAsync(id);
            if (calificacion == null) return NotFound();

            _repositorio.Context.Calificaciones.Remove(calificacion);
            await _repositorio.Context.SaveChangesAsync();
            return NoContent();
        }
    }
}