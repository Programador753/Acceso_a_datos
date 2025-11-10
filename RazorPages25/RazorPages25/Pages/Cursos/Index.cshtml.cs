using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Pages.Cursos
{
    public class IndexModel : PageModel
    {
        private readonly ProfesorRepositorio _profesorRepositorio;

        public List<Curso> Cursos { get; set; }
        public List<SelectListItem> Profesores { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? ProfesorSeleccionado { get; set; }

        public IndexModel(ProfesorRepositorio profesorRepositorio)
        {
            _profesorRepositorio = profesorRepositorio;
        }

        public void OnGet()
        {
            // Cargar la lista de profesores para el dropdown
            Profesores = _profesorRepositorio.GetAllProfesores()
                .Select(p => new SelectListItem
                {
                    Value = p.ID.ToString(),
                    Text = p.NomProfesor
                })
                .ToList();

            // Insertar opción "Todos" al inicio
            Profesores.Insert(0, new SelectListItem { Value = "", Text = "-- Todos los profesores --" });

            // Filtrar cursos según el profesor seleccionado
            if (ProfesorSeleccionado.HasValue)
            {
                Cursos = _profesorRepositorio.GetCursosPorProfesor(ProfesorSeleccionado.Value).ToList();
            }
            else
            {
                // Obtener todos los valores del enum Curso
                Cursos = Enum.GetValues(typeof(Curso)).Cast<Curso>().ToList();
            }
        }
    }
}