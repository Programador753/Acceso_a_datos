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
    public class AreasController : ControllerBase
    {
        private readonly Contexto _context;

        public AreasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Area
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetArea()
        {
            if (_context.Area == null)
            {
                return NotFound();
            }
            return await _context.Area
                .Include(a => a.Meals)
                    .ThenInclude(m => m.Category)
                .Include(a => a.Meals)
                    .ThenInclude(m => m.MealIngredients)
                        .ThenInclude(mi => mi.Ingredient)
                .ToListAsync();
        }

        // GET: api/Area/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
            if (_context.Area == null)
            {
                return NotFound();
            }
            var area = await _context.Area
                .Include(a => a.Meals)
                    .ThenInclude(m => m.Category)
                .Include(a => a.Meals)
                    .ThenInclude(m => m.MealIngredients)
                        .ThenInclude(mi => mi.Ingredient)
                .FirstOrDefaultAsync(a => a.IdArea == id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        // PUT: api/Area/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(int id, Area area)
        {
            if (id != area.IdArea)
            {
                return BadRequest();
            }

            _context.Entry(area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
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

        // POST: api/Area
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
            if (_context.Area == null)
            {
                return Problem("Entity set 'Contexto.Area'  is null.");
            }
            _context.Area.Add(area);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArea", new { id = area.IdArea }, area);
        }

        // DELETE: api/Area/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            if (_context.Area == null)
            {
                return NotFound();
            }
            var area = await _context.Area.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            _context.Area.Remove(area);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AreaExists(int id)
        {
            return (_context.Area?.Any(e => e.IdArea == id)).GetValueOrDefault();
        }
    }
}