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
    public class MealsController : ControllerBase
    {
        private readonly Contexto _context;

        public MealsController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Meals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeal()
        {
            if (_context.Meal == null)
            {
                return NotFound();
            }
            return await _context.Meal
                .Include(m => m.Category)
                .Include(m => m.Area)
                .Include(m => m.MealIngredients)
                    .ThenInclude(mi => mi.Ingredient)
                .ToListAsync();
        }

        // GET: api/Meals/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            if (_context.Meal == null)
            {
                return NotFound();
            }
            var meal = await _context.Meal
                .Include(m => m.Category)
                .Include(m => m.Area)
                .Include(m => m.MealIngredients)
                    .ThenInclude(mi => mi.Ingredient)
                .FirstOrDefaultAsync(m => m.IdMeal == id);

            if (meal == null)
            {
                return NotFound();
            }

            return meal;
        }

        // GET: api/Meals/category/5
        [HttpGet("category/{idCategory:int}")]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMealsByCategory(int idCategory)
        {
            if (_context.Meal == null)
            {
                return NotFound();
            }

            var meals = await _context.Meal
                .Where(m => m.IdCategory == idCategory)
                .Include(m => m.Category)
                .Include(m => m.Area)
                .Include(m => m.MealIngredients)
                    .ThenInclude(mi => mi.Ingredient)
                .ToListAsync();

            if (meals == null || !meals.Any())
            {
                return NotFound(new { message = $"No se encontraron platos para la categoría con ID {idCategory}" });
            }

            return meals;
        }

        // PUT: api/Meals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(int id, Meal meal)
        {
            if (id != meal.IdMeal)
            {
                return BadRequest();
            }

            _context.Entry(meal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
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

        // POST: api/Meals
        [HttpPost]
        public async Task<ActionResult<Meal>> PostMeal(Meal meal)
        {
            if (_context.Meal == null)
            {
                return Problem("Entity set 'Contexto.Meal' is null.");
            }

            // Guardar el plato principal
            _context.Meal.Add(meal);
            await _context.SaveChangesAsync();

            // Guardar los ingredientes en la tabla intermedia
            if (meal.IngredientsSeleccionados != null && meal.IngredientsSeleccionados.Any())
            {
                foreach (var idIngredient in meal.IngredientsSeleccionados)
                {
                    var mealIngredient = new MealIngredient
                    {
                        IdMeal = meal.IdMeal,
                        IdIngredient = idIngredient,
                        Measure = null // Se omite Measure según lo solicitado
                    };
                    _context.MealIngredient.Add(mealIngredient);
                }
                await _context.SaveChangesAsync();
            }

            await _context.Entry(meal)
                .Reference(m => m.Category)
                .LoadAsync();
            await _context.Entry(meal)
                .Reference(m => m.Area)
                .LoadAsync();
            await _context.Entry(meal)
                .Collection(m => m.MealIngredients)
                .LoadAsync();

            foreach (var mi in meal.MealIngredients)
            {
                await _context.Entry(mi)
                    .Reference(x => x.Ingredient)
                    .LoadAsync();
            }

            return CreatedAtAction("GetMeal", new { id = meal.IdMeal }, meal);
        }

        // DELETE: api/Meals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            if (_context.Meal == null)
            {
                return NotFound();
            }
            var meal = await _context.Meal.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meal.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealExists(int id)
        {
            return (_context.Meal?.Any(e => e.IdMeal == id)).GetValueOrDefault();
        }
    }
}