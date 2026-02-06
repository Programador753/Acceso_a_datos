using ColegioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioAPI.Data
{
    public class CalificacionRepositorio
    {
        public MyContext Context { get; }
        public CalificacionRepositorio(MyContext context)
        {
            Context = context;
        }

        public async Task<List<Calificacion>> ObtenerCalificacionesAsync()
        {
            return await Context.Calificaciones.Include(c => c.alumno).ThenInclude(a => a.Pais)
                .Include(c => c.asignatura).ThenInclude(a => a.profesor).ToListAsync();
        }
    }
}
