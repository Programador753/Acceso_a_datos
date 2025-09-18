using RazorPages.Modelos;

namespace RazorPages.Services
{
    public interface IAlumnoRepositorio
    {
        IEnumerable<Alumno> GetAllAlumnos();
    }
}
