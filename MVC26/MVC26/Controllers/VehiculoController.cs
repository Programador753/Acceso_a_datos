using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC26.Models;

namespace MVC26.Controllers
{
    public class VehiculoController : Controller
    {
        public class VehiculoTotal
        {
            public string Nom_Marca { get; set; }
            public string Nom_Serie { get; set; }
            public string Matricula { get; set; }
            public string Color { get; set; }

        }

        public ActionResult Listado2()
        {
            List<VehiculoTotal> lista = Contexto.VistaTotal.FromSql($"SELECT dbo.Marcas.Nom_Marca, dbo.Series.Nom_Serie, dbo.Vehiculos.Matricula, dbo.Vehiculos.Color FROM dbo.Marcas INNER JOIN dbo.Series ON dbo.Marcas.ID = dbo.Series.MarcaID INNER JOIN dbo.Vehiculos ON dbo.Series.ID = dbo.Vehiculos.SerieID").ToList();
            //List<VehiculoTotal> lista = Contexto.VistaTotal.ToList();
            return View(lista);
        }

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
            ViewBag.TextoBusqueda = busca;
            var lista = from v in Contexto.Vehiculos where (v.Matricula.Contains(busca)) select v;
            return View(lista.Include(v => v.Serie).ThenInclude(s => s.Marca));
        }

        // GET: VehiculoController/Busqueda2  
        public IActionResult Busqueda2(string busca = "")
        {
            ViewBag.Matriculas = new SelectList(Contexto.Vehiculos, "Matricula", "Matricula", busca);
            var lista = (from v in Contexto.Vehiculos where ( v.Matricula == busca) select v);
            return View(lista.Include(v => v.Serie).ThenInclude(s => s.Marca));
        }

        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
        {
            VehiculoModelo vehiculo = Contexto.Vehiculos.Include(v => v.Serie).ThenInclude(s => s.Marca).FirstOrDefault(v => v.ID == id);
            return View(vehiculo);
        }

        // GET: VehiculoController/Listado (Pantalla con filtro de select Marcas y select Series)
        public ActionResult Listado(int? marcaId, int? serieId)
        {
            ViewBag.Marcas = new SelectList(Contexto.Marcas, "ID", "Nom_Marca", marcaId);
            ViewBag.Series = new SelectList(Contexto.Series.Where(s => s.MarcaID == marcaId), "ID", "Nom_Serie", serieId);

            var lista = Contexto.Vehiculos.Include(v => v.Serie).ThenInclude(s => s.Marca).AsQueryable();
            if (marcaId.HasValue)
            {
                lista = lista.Where(v => v.Serie.MarcaID == marcaId.Value);
            }
            if (serieId.HasValue)
            {
                lista = lista.Where(v => v.SerieID == serieId.Value);
            }
            return View(lista);
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
            VehiculoModelo vehiculo = Contexto.Vehiculos.Include(v => v.Serie).ThenInclude(s => s.Marca).FirstOrDefault(v => v.ID == id);
            return View(vehiculo);
        }

        // POST: VehiculoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                VehiculoModelo vehiculo = Contexto.Vehiculos.Find(id);
                Contexto.Vehiculos.Remove(vehiculo);
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
