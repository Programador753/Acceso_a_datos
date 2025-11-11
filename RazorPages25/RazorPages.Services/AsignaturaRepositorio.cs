using Microsoft.EntityFrameworkCore;
using RazorPages.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<Asignatura> GetAsignaturasCurso(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return Context.Asignaturas;
            }
            return Context.Asignaturas.Where(a => a.nomAsignatura.Contains(nombre));
        }

        public IEnumerable<Asignatura> GetAsignaturasCurso(Curso codigo)
        {
            return Context.Asignaturas.Where(a => a.cursoId == codigo);
        }

        public IEnumerable<Asignatura> GetAsignaturasPorProfesor(int profesorId)
        {
            return Context.Asignaturas
                .Where(a => a.profeID == profesorId)
                .Include(a => a.Profesor);
        }
    }
}
