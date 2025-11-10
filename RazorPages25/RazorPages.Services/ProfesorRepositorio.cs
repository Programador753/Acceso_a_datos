using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorPages.Modelos;

namespace RazorPages.Services
{
    public class ProfesorRepositorio
    {
        public ColegioDbContext Context { get; }

        public ProfesorRepositorio(ColegioDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Profesor> GetAllProfesores()
        {
            return Context.Profesores;
        }

        public IEnumerable<Profesor> GetProfesoresCurso(Curso curso)
        {
            return Context.Asignaturas
                .Where(a => a.cursoId == curso)
                .Select(a => a.profeID)
                .Distinct()
                .Join(Context.Profesores,
                    profeID => profeID,
                    profesor => profesor.ID,
                    (profeID, profesor) => profesor);
        }
        public IEnumerable<Curso> GetCursosPorProfesor(int profesorId)
        {
            return Context.Asignaturas
                .Where(a => a.profeID == profesorId)
                .Select(a => a.cursoId)
                .Distinct();
        }
    }
}
