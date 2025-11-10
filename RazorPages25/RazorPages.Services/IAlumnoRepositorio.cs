using RazorPages.Modelos;

namespace RazorPages.Services
{
    public interface IAlumnoRepositorio
    {
        IEnumerable<Alumno> GetAllAlumnos(); // Obtener todos los alumnos
        Alumno GetAlumno(int id); // Obtener un alumno por id 
        void Update(Alumno alumnoActualizado); // Actualizar un alumno existente
        void Add(Alumno alumnoNuevo); // Agregar un nuevo alumno 
        void Delete(int id); // Eliminar un alumno por id 

        public IEnumerable<CursoCuantos> AlumnoPorCurso(Curso? curso); // Cantidad de alumnos por curso
        public IEnumerable<Alumno> Busqueda(string elementoABuscar); // Búsqueda de alumnos por nombre o apellido
        public IEnumerable<Alumno> GetAlumnosCurso(Curso curso); // Obtener alumnos por curso desde el repositorio
    }
}
