using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntonioHernandezAPI.Data;
using AntonioHernandezAPI.Models;

namespace AntonioHernandezAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly Contexto _context;

        public CategoriasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCategorias()
        {
            if (_context.Categoria == null)
                return NotFound();

            return await _context.Categoria
                .Select(c => new { c.Id, c.Nombre })
                .ToListAsync<object>();
        }
    }
}
