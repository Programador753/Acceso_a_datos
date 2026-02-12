using AntonioMVCv1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AntonioMVCv1.Controllers
{
    public class AutorController : Controller
    {
        public Contexto Contexto { get; }

        public AutorController(Contexto contexto)
        {
            Contexto = contexto;
        }
        // GET: AutorController
        public ActionResult Index()
        {
            var autores = Contexto.Autores
                .Include(a => a.pais)
                .Include(a => a.AutorPremios) // Load the list added to the Model above
                .ThenInclude(ap => ap.premio) // Load the actual 'Premio' inside AutorPremio
                .ToList();

            return View(autores);
        }

        // GET: AutorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AutorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AutorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AutorController/Edit/5
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

        // GET: AutorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AutorController/Delete/5
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
