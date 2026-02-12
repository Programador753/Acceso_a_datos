using AntonioMVCv1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AntonioMVCv1.Controllers
{
    public class PremioController : Controller
    {
        public Contexto Contexto { get; }

        public PremioController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: PremioController
        public ActionResult Index()
        {
            List<Premio> premios = Contexto.Premios.Include("pais").ToList();
            return View(premios);

        }

        // GET: PremioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PremioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PremioController/Create
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

        // GET: PremioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PremioController/Edit/5
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

        // GET: PremioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PremioController/Delete/5
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
