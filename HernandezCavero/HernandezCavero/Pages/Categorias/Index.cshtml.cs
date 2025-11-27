using HernadezCavero.Services;
using HernandezCavero.Components;
using HernandezCavero.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HernandezCavero.Pages.Categorias
{
    public class IndexModel : PageModel
    {
        private readonly ProductosRepositorio _productosRepositorio;
        private readonly CategoriasRepositorio _categoriasRepositorio;

        public List<Producto> Productos { get; set; }
        public List<SelectListItem> Categorias { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoriaSeleccionada { get; set; }

        public IndexModel(ProductosRepositorio productosRepositorio, CategoriasRepositorio categoriasRepositorio)
        {
            _productosRepositorio = productosRepositorio;
            _categoriasRepositorio = categoriasRepositorio;
        }

        public void OnGet()
        {
            Categorias = _categoriasRepositorio.GetAllCategorias()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                }).ToList();

            Categorias.Insert(0, new SelectListItem { Value = "", Text = "-- Todas las categorías --" });

            if (CategoriaSeleccionada.HasValue)
            {
                Productos = _productosRepositorio.GetProductosCategoria(CategoriaSeleccionada.Value).ToList();
            }
            else
            {
                Productos = _productosRepositorio.GetAllProductos().ToList();
            }
        }
    }
}