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
    public class CategorysController : ControllerBase
    {
        private readonly Contexto _context;

        public CategorysController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            return await _context.Category
                .Include(c => c.Meals)
                    .ThenInclude(m => m.Area)
                .Include(c => c.Meals)
                    .ThenInclude(m => m.MealIngredients)
                        .ThenInclude(mi => mi.Ingredient)
                .ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            var category = await _context.Category
                .Include(c => c.Meals)
                    .ThenInclude(m => m.Area)
                .Include(c => c.Meals)
                    .ThenInclude(m => m.MealIngredients)
                        .ThenInclude(mi => mi.Ingredient)
                .FirstOrDefaultAsync(c => c.IdCategory == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }


        // PUT: api/Categories/5
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.IdCategory)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'Contexto.Category'  is null.");
            }
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.IdCategory }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Category?.Any(e => e.IdCategory == id)).GetValueOrDefault();
        }
    }
}