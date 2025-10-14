using RazorPages.Modelos;

namespace RazorPages.Services
{
    public interface IAlumnoRepositorio
    {
        IEnumerable<Alumno> GetAllAlumnos();
        Alumno GetAlumno(int id);
        void Update(Alumno alumnoActualizado); 
        void Add(Alumno alumnoNuevo);
        void Delete(int id);

        public IEnumerable<CursoCuantos> AlumnoPorCurso();
    }
}
