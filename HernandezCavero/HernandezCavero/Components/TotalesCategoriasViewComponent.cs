using HernadezCavero.Services;
using HernandezCavero.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace HernandezCavero.Components
{
    public class TotalesCategoriasViewComponent : ViewComponent
    {
        private readonly ExamenDbContext _context;

        public TotalesCategoriasViewComponent(ExamenDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var totalesPorCategoria = _context.Categorias
                .Select(c => new TotalCategoriaViewModel
                {
                    NombreCategoria = c.Nombre,
                    TotalVendido = _context.Ventas
                        .Where(v => v.Producto.CategoriaId == c.Id)
                        .Sum(v => v.Cantidad * v.Producto.Precio)
                })
                .ToList();

            return View(totalesPorCategoria);
        }
    }

    public class TotalCategoriaViewModel
    {
        public string NombreCategoria { get; set; }
        public decimal TotalVendido { get; set; }
    }
}