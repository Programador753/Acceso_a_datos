using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Pages.Calificaciones
{
    public class InsertarModel : PageModel
    {
        public List<Curso> Cursos { get; set; }
        public List<Convocatoria> convocatorias { get; set; }

        [BindProperty(SupportsGet = true)]
        public Calificacion calificacion { get; set; }

        [BindProperty(SupportsGet = true)]
        public Curso curso { get; set; }

        [BindProperty(SupportsGet = true)]
        public int asignaturaID { get; set; }
        public CalificacionRepositorio CalificacionRepositorio { get; }
        public AsignaturaRepositorio AsignaturaRepositorio { get; private set; }
        public IAlumnoRepositorio AlumnoRepositorio { get; }
        public List<Asignatura> asignaturas { get; set; }
        public List<Alumno> alumnos { get; set; }
        public List<Calificacion> CalificacionesMostradas { get; set; } = new();

        public InsertarModel(CalificacionRepositorio calificacionRepositorio, AsignaturaRepositorio asignaturaRepositorio, IAlumnoRepositorio alumnoRepositorio)
        {
            CalificacionRepositorio = calificacionRepositorio;
            AsignaturaRepositorio = asignaturaRepositorio;
            AlumnoRepositorio = alumnoRepositorio;
        }
        public void OnGet()
        {
            Cursos = Enum.GetValues(typeof(Curso)).Cast<Curso>().ToList();
            convocatorias = Enum.GetValues(typeof(Convocatoria)).Cast<Convocatoria>().ToList();
            asignaturas = AsignaturaRepositorio.GetAsignaturasCurso(curso).ToList();
            alumnos = AlumnoRepositorio.GetAlumnosCurso(curso).ToList();
            if (asignaturaID != 0)
                calificacion.asignaturaID = asignaturaID;
            CalificacionesMostradas = CalificacionRepositorio.GetClaCalificacionesConvAsign(calificacion.convocatoria, asignaturaID).ToList();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            CalificacionRepositorio.insertar(calificacion);

            TempData["mensaje"] = "Calificación insertada correctamente.";

            return RedirectToPage(new
            {
                calificacion.convocatoria,
                curso,
                calificacion.asignaturaID
            }); // vuelve a limpiar la página
        }
    }
}
