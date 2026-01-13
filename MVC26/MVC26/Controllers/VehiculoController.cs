using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC26.Models;

namespace MVC26.Controllers
{
    public class VehiculoController : Controller
    {
        public Contexto Contexto { get; }
        public VehiculoController(Contexto contexto)
        {
            Contexto = contexto;
        }
        // GET: VehiculoController
        public ActionResult Index()
        {
            var losCoches = Contexto.Vehiculos.Include(v => v.Serie).ThenInclude(s => s.Marca);
            return View(losCoches);
        }
        // GET: VehiculoController/Busqueda
        public ActionResult Busqueda(string busca = "")
        {
            var lista = from v in Contexto.Vehiculos where (v.Matricula.Contains(busca)) select v;
            return View(lista.Include(v => v.Serie).ThenInclude(s => s.Marca));
        }

        // GET: VehiculoController/Busqueda2  
        public IActionResult Busqueda2(string busca)
        {
            // Obtener todas las matrículas únicas
            ViewBag.Matriculas = _context.VehiculoModelo
                .Select(v => v.Matricula)
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            // Filtrar vehículos si hay búsqueda
            var vehiculos = string.IsNullOrEmpty(busca)
                ? _context.VehiculoModelo.Include(v => v.Serie).ToList()
                : _context.VehiculoModelo.Include(v => v.Serie)
                    .Where(v => v.Matricula == busca)
                    .ToList();

            return View(vehiculos);
        }

        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehiculoController/Create
        public ActionResult Create()
        {
            ViewBag.SerieID = new SelectList(Contexto.Series, "ID", "Nom_Serie");
            return View();
        }

        // POST: VehiculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehiculoModelo vehiculo)
        {
            try
            {
                Contexto.Vehiculos.Add(vehiculo);
                Contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehiculoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehiculoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehiculoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehiculoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
