using HernadezCavero.Services;
using HernandezCavero.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HernandezCavero.Components
{
    public class InsertarVentaViewComponent : ViewComponent
    {
        private readonly ExamenDbContext _context;

        public InsertarVentaViewComponent(ExamenDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int? categoriaId = null)
        {
            var model = new InsertarVentaViewModel
            {
                CategoriaId = categoriaId,
                FechaVenta = DateTime.Today
            };

            // Cargar categorías
            model.Categorias = _context.Categorias
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                }).ToList();

            model.Categorias.Insert(0, new SelectListItem { Value = "", Text = "-- Selecciona categoría --" });

            // Cargar productos si hay categoría seleccionada
            if (categoriaId.HasValue)
            {
                model.Productos = _context.Productos
                    .Where(p => p.CategoriaId == categoriaId.Value)
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = $"{p.Nombre} - {p.Precio:C}"
                    }).ToList();
            }
            else
            {
                model.Productos = new List<SelectListItem>();
            }

            model.Productos.Insert(0, new SelectListItem { Value = "", Text = "-- Selecciona un producto --" });

            return View(model);
        }
    }

    public class InsertarVentaViewModel
    {
        public List<SelectListItem> Categorias { get; set; }
        public List<SelectListItem> Productos { get; set; }
        public int? CategoriaId { get; set; }
        public int? ProductoId { get; set; }
        public int? Cantidad { get; set; }
        public string Cliente { get; set; }
        public DateTime FechaVenta { get; set; }
    }
}