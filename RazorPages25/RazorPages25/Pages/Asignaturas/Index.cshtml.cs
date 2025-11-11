using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Pages.Asignaturas
{
    public class IndexModel : PageModel
    {
        private readonly AsignaturaRepositorio _asignaturaRepositorio;
        private readonly ProfesorRepositorio _profesorRepositorio;

        public List<Asignatura> Asignaturas { get; set; }
        public string ElementoABuscar { get; set; }

        public List<SelectListItem> Profesores { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? ProfesorSeleccionado { get; set; }
        public IndexModel(AsignaturaRepositorio asignaturaRepositorio, ProfesorRepositorio profesorRepositorio)
        {
            _asignaturaRepositorio = asignaturaRepositorio;
            _profesorRepositorio = profesorRepositorio;
        }

        public void OnGet(string elementoABuscar = "")
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

            if (ProfesorSeleccionado.HasValue)
            {
                Asignaturas = _asignaturaRepositorio.GetAsignaturasPorProfesor(ProfesorSeleccionado.Value).ToList();
                return; // Salir del método OnGet después de filtrar por profesor seleccionad
            }
            else
            {
                // Si no hay profesor seleccionado, obtener todas las asignaturas
                if (string.IsNullOrEmpty(elementoABuscar))
                {
                    Asignaturas = _asignaturaRepositorio.GetAllAsignaturas().ToList();
                    ElementoABuscar = string.Empty;
                    return;
                }
                Asignaturas = _asignaturaRepositorio.GetAsignaturasCurso(elementoABuscar).ToList();
                ElementoABuscar = elementoABuscar;
                }
            }
        }
    }
