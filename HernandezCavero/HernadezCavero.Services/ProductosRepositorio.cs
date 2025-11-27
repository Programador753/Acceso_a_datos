using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HernandezCavero.Modelos;
using Microsoft.EntityFrameworkCore;

namespace HernadezCavero.Services
{
    public class ProductosRepositorio
    {
        public ExamenDbContext Context { get; set; }

        public ProductosRepositorio(ExamenDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Producto> GetAllProductos()
        {
            return Context.Productos;
        }

        public IEnumerable<Producto> GetProductosCategoria(int id)
        {
            return Context.Productos.Where(p => p.CategoriaId == id)
                .Include(p => p.Categoria);
        }

        public IEnumerable<Producto> GetProductosPorCategoria(Categoria id)
        {
            return Context.Productos.Where(p => p.Categoria == id);
        }
    }
}
