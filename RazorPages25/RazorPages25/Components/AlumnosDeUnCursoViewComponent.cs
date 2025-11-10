using Microsoft.AspNetCore.Mvc;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Components
{
    public class AlumnosDeUnCursoViewComponent : ViewComponent // Definición de la clase AlumnosDeUnCursoViewComponent que hereda de ViewComponent
    {
        public IAlumnoRepositorio AlumnoRepositorio { get; } // Propiedad pública para el repositorio de alumnos

        public AlumnosDeUnCursoViewComponent(IAlumnoRepositorio alumnoRepositorio) // Constructor que recibe una instancia de IAlumnoRepositorio
        {
           AlumnoRepositorio = alumnoRepositorio; // Asignación del repositorio a la propiedad
        }
        public IViewComponentResult Invoke(Curso curso) // Método Invoke que recibe un curso opcional y devuelve una vista con los alumnos del curso
        {
            var resultado = AlumnoRepositorio.GetAlumnosCurso(curso); // Obtener los alumnos del curso especificado
            return View(resultado); // Devolver la vista con los alumnos obtenidos
        }
    }
}
