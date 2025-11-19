using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Pages.Calificaciones
{
    /// <summary>
    /// Modelo de página para insertar calificaciones de alumnos
    /// </summary>
    public class insertarModel : PageModel
    {
        #region Propiedades y Dependencias

        // Repositorio para acceder a las asignaturas
        public AsignaturaRepositorio AsignaturaRepositorio { get; private set; }

        // Repositorio para acceder a los alumnos
        public IAlumnoRepositorio AlumnoRepositorio { get; }

        #endregion

        #region Propiedades de Datos de la Página

        // Lista de convocatorias disponibles (EV1, EV2, EV3, CV1, CV2, CV3, CV4)
        public List<Convocatoria> convocatoria { get; set; }

        // Lista de cursos disponibles (E1, E2, H1, H2)
        public List<Curso> Cursos { get; set; }

        // Lista de asignaturas filtradas por el curso seleccionado
        public List<Asignatura> asignaturas { get; set; }

        // Lista de alumnos filtrados por el curso seleccionado
        public List<Alumno> alumnos { get; set; }

        #endregion

        #region Propiedades Enlazadas (BindProperty)

        // Curso seleccionado desde la URL o formulario
        [BindProperty(SupportsGet = true)]
        public Curso curso { get; set; }

        // Objeto calificación que se creará/modificará
        [BindProperty(SupportsGet = true)]
        public Calificacion calificacion { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor con inyección de dependencias
        /// </summary>
        /// <param name="asignaturaRepositorio">Repositorio de asignaturas</param>
        /// <param name="alumnoRepositorio">Repositorio de alumnos</param>
        public insertarModel(AsignaturaRepositorio asignaturaRepositorio, IAlumnoRepositorio alumnoRepositorio)
        {
            AsignaturaRepositorio = asignaturaRepositorio;
            AlumnoRepositorio = alumnoRepositorio;
        }

        #endregion

        #region Métodos de Página

        /// <summary>
        /// Manejador GET: carga los datos iniciales para el formulario de inserción
        /// </summary>
        public void OnGet()
        {
            // Cargar todas las convocatorias disponibles desde el enum
            convocatoria = Enum.GetValues(typeof(Convocatoria)).Cast<Convocatoria>().ToList();

            // Cargar todos los cursos disponibles desde el enum
            Cursos = Enum.GetValues(typeof(Curso)).Cast<Curso>().ToList();

            // Filtrar asignaturas según el curso seleccionado
            asignaturas = AsignaturaRepositorio.GetAsignaturasCurso(curso).ToList();

            // Filtrar alumnos según el curso seleccionado
            alumnos = AlumnoRepositorio.GetAlumnosCurso(curso).ToList();
        }

        #endregion
    }
}