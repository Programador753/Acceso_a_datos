using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntonioHernandezAPI.Data;
using AntonioHernandezAPI.Models;

namespace AntonioHernandezAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly Contexto _context;

        public NoticiasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Noticias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticia()
        {
            if (_context.Noticia == null)
            {
                return NotFound();
            }
            return await _context.Noticia
                .Include(n => n.ImagenNoticias)
                .Include(n => n.NoticiaCategorias)
                    .ThenInclude(nc => nc.Categoria)
                .ToListAsync();
        }

        // GET: api/Noticias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Noticia>> GetNoticia(int id)
        {
            if (_context.Noticia == null)
            {
                return NotFound();
            }
            var noticia = await _context.Noticia
                .Include(n => n.ImagenNoticias)
                .Include(n => n.NoticiaCategorias)
                    .ThenInclude(nc => nc.Categoria)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (noticia == null)
            {
                return NotFound();
            }

            return noticia;
        }

        // PUT: api/Noticias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoticia(int id, Noticia noticia)
        {
            if (id != noticia.Id)
            {
                return BadRequest();
            }

            _context.Entry(noticia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Noticias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Noticia>> PostNoticia(Noticia noticia)
        {
            if (_context.Noticia == null)
            {
                return Problem("Entity set 'Contexto.Noticia'  is null.");
            }
            _context.Noticia.Add(noticia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoticia", new { id = noticia.Id }, noticia);
        }

        // DELETE: api/Noticias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoticia(int id)
        {
            if (_context.Noticia == null)
            {
                return NotFound();
            }
            var noticia = await _context.Noticia.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }

            _context.Noticia.Remove(noticia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoticiaExists(int id)
        {
            return (_context.Noticia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}