using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Pages.Calificaciones
{
    public class insertarModel : PageModel
    { 
        public List<Convocatoria> Convocatoria { get; set; }
        public List<Curso> Cursos { get; set; }
        [BindProperty(SupportsGet = true)]
        public Curso curso { get; set; }
        public AsignaturaRepositorio AsignaturaRepositorio { get; private set; }
        public List<Asignatura> asignaturas { get; set; }
        [BindProperty]
        public int asignatura { get; set; }
        public insertarModel(AsignaturaRepositorio asignaturaRepositorio, IAlumnoRepositorio alumnoRepositorio)
        {
            AsignaturaRepositorio = asignaturaRepositorio;
            AlumnoRepositorio = alumnoRepositorio;
        }
        public IAlumnoRepositorio AlumnoRepositorio { get; private set; }

        public List<Alumno> alumnos { get; set; }
        public void OnGet()
        {
            // Obtener todos los valores del enum Convocatoria y Curso
            Convocatoria = Enum.GetValues(typeof(Convocatoria)).Cast<Convocatoria>().ToList();
            Cursos = Enum.GetValues(typeof(Curso)).Cast<Curso>().ToList();
            asignaturas = AsignaturaRepositorio.GetAsignaturasCurso(curso).ToList();
            // Corregido: Usar GetAlumnosCurso para obtener List<Alumno>
            alumnos = AlumnoRepositorio.GetAlumnosCurso(curso).ToList();
        }
    }
}
