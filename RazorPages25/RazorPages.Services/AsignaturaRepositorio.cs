using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorPages.Modelos;

namespace RazorPages.Services
{
    public class AsignaturaRepositorio
    {
        public ColegioDbContext Context { get; }
        public AsignaturaRepositorio(ColegioDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Asignatura> GetAllAsignaturas()
        {
            return Context.Asignaturas;
        }
    }
}
