using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HernandezCavero.Modelos;

namespace HernadezCavero.Services
{
    public class CategoriasRepositorio 
    {
        public ExamenDbContext Context { get; set; }

        public CategoriasRepositorio(ExamenDbContext context)
        {
            Context = context;
        }

        public void insertar(Categoria categoria)
        {
            Context.Categorias.Add(categoria);
            Context.SaveChanges();
        }

        public IEnumerable<Categoria> GetAllCategorias()
        {
            return Context.Categorias;
        }
    }
}
