using AntonioMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AntonioMVC.Controllers
{
    public class MealController : Controller
    {
        public Contexto Contexto { get; }

        public MealController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: MealController
        public ActionResult Index()
        {
            // Obtener meals con sus relaciones
            var meals = Contexto.Meal
                .Include(m => m.Category)
                .Include(m => m.Area)
                .Include(m => m.MealIngredients)
                .ThenInclude(mi => mi.Ingredient)
                .ToList();

            return View(meals);
        }

        // GET: MealController/Details/5
        public ActionResult Details(int id)
        {
            var meal = Contexto.Meal
                .Include(m => m.Category)
                .Include(m => m.Area)
                .Include(m => m.MealIngredients)
                    .ThenInclude(mi => mi.Ingredient)
                .FirstOrDefault(m => m.IdMeal == id);

            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: MealController/Create
        public ActionResult Create()
        {
            // Cargamos las categorías, áreas e ingredientes para los selects de la vista
            ViewBag.CategoryIds = new SelectList(Contexto.Category, "IdCategory", "Name");
            ViewBag.AreaIds = new SelectList(Contexto.Area, "IdArea", "Name");
            ViewBag.IngredientIds = new SelectList(Contexto.Ingredient, "IdIngredient", "Name");

            return View();
        }

        // POST: MealController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Meal meal)
        {
            try
            {
                Contexto.Meal.Add(meal);
                Contexto.SaveChanges();

                // Verificar que IngredientsSeleccionados no sea null y contenga elementos
                if (meal.IngredientsSeleccionados != null && meal.IngredientsSeleccionados.Any())
                {
                    foreach (var ingredientId in meal.IngredientsSeleccionados)
                    {
                        var obj = new MealIngredient()
                        {
                            IdMeal = meal.IdMeal,
                            IdIngredient = ingredientId,
                            Measure = "" // Campo vacío según requisitos
                        };
                        Contexto.MealIngredient.Add(obj);
                    }
                    Contexto.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Si hay un error de base de datos, recargamos los selects y volvemos a la vista
                ViewBag.CategoryIds = new SelectList(Contexto.Category, "IdCategory", "Name");
                ViewBag.AreaIds = new SelectList(Contexto.Area, "IdArea", "Name");
                ViewBag.IngredientIds = new SelectList(Contexto.Ingredient, "IdIngredient", "Name");
                return View(meal);
            }
        }

        // GET: MealController/Edit/5
        public ActionResult Edit(int id)
        {
            var meal = Contexto.Meal
                .Include(m => m.MealIngredients)
                .FirstOrDefault(m => m.IdMeal == id);

            if (meal == null)
            {
                return NotFound();
            }

            // Cargar los ingredientes ya seleccionados
            meal.IngredientsSeleccionados = meal.MealIngredients
                .Select(mi => mi.IdIngredient)
                .ToList();

            ViewBag.CategoryIds = new SelectList(Contexto.Category, "IdCategory", "Name", meal.IdCategory);
            ViewBag.AreaIds = new SelectList(Contexto.Area, "IdArea", "Name", meal.IdArea);
            ViewBag.IngredientIds = new SelectList(Contexto.Ingredient, "IdIngredient", "Name");

            return View(meal);
        }

        // POST: MealController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Meal meal)
        {
            try
            {
                if (id != meal.IdMeal)
                {
                    return BadRequest();
                }

                // Actualizar el meal
                Contexto.Meal.Update(meal);
                Contexto.SaveChanges();

                // Eliminar las relaciones anteriores
                var ingredientesAnteriores = Contexto.MealIngredient
                    .Where(mi => mi.IdMeal == id)
                    .ToList();
                Contexto.MealIngredient.RemoveRange(ingredientesAnteriores);
                Contexto.SaveChanges();

                // Agregar las nuevas relaciones
                if (meal.IngredientsSeleccionados != null && meal.IngredientsSeleccionados.Any())
                {
                    foreach (var ingredientId in meal.IngredientsSeleccionados)
                    {
                        var obj = new MealIngredient()
                        {
                            IdMeal = meal.IdMeal,
                            IdIngredient = ingredientId,
                            Measure = ""
                        };
                        Contexto.MealIngredient.Add(obj);
                    }
                    Contexto.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.CategoryIds = new SelectList(Contexto.Category, "IdCategory", "Name", meal.IdCategory);
                ViewBag.AreaIds = new SelectList(Contexto.Area, "IdArea", "Name", meal.IdArea);
                ViewBag.IngredientIds = new SelectList(Contexto.Ingredient, "IdIngredient", "Name");
                return View(meal);
            }
        }

        // GET: MealController/Delete/5
        public ActionResult Delete(int id)
        {
            var meal = Contexto.Meal
                .Include(m => m.Category)
                .Include(m => m.Area)
                .Include(m => m.MealIngredients)
                    .ThenInclude(mi => mi.Ingredient)
                .FirstOrDefault(m => m.IdMeal == id);

            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: MealController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var meal = Contexto.Meal.Find(id);
                if (meal == null)
                {
                    return NotFound();
                }

                // Eliminar las relaciones primero
                var mealIngredients = Contexto.MealIngredient
                    .Where(mi => mi.IdMeal == id)
                    .ToList();
                Contexto.MealIngredient.RemoveRange(mealIngredients);

                // Eliminar el meal
                Contexto.Meal.Remove(meal);
                Contexto.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}