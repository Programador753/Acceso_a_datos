using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorPages.Modelos;

namespace RazorPages.Services
{
   public class CalificacionRepositorio
   {
      public ColegioDbContext Context { get; set; }

      public CalificacionRepositorio(ColegioDbContext context)
      {
          Context = context;
      }
      public void insertar(Calificacion calificacion)
      {
          Context.Calificaciones.Add(calificacion);
          Context.SaveChanges();
      }

      public IEnumerable<Calificacion> GetClaCalificacionesConvAsign(Convocatoria convocatoria, int asignatura)
      { 
          return Context.Calificaciones
              .Where(c => c.asignaturaID == asignatura && c.convocatoria == convocatoria)
              .ToList();
      }

    }
}
