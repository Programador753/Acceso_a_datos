using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC26.Models;

namespace MVC26.Controllers
{
    public class SerieController : Controller
    {
        public Contexto Contexto { get; }
        public SerieController(Contexto contexto)
        {
            Contexto = contexto;
        }
        // GET: SerieController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SerieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SerieController/Create
        public ActionResult Create()
        {
            ViewBag.MarcaID = new SelectList(Contexto.Marcas, "ID", "Nom_Marca");
            return View();
        }

        // POST: SerieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: SerieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SerieController/Edit/5
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

        // GET: SerieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SerieController/Delete/5
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
