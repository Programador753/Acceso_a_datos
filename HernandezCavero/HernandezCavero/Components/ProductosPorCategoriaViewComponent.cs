using HernadezCavero.Services;
using HernandezCavero.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace HernandezCavero.Components
{
    public class ProductosPorCategoriaViewComponent : ViewComponent
    {
        private readonly ProductosRepositorio _productosRepositorio;

        public ProductosPorCategoriaViewComponent(ProductosRepositorio productosRepositorio)
        {
            _productosRepositorio = productosRepositorio;
        }

        public IViewComponentResult Invoke(Categoria categoria)
        {
            var productos = _productosRepositorio.GetProductosPorCategoria(categoria);
            return View(productos);
        }
    }
}