using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Pages.Asignaturas
{
    public class IndexModel : PageModel
    {
        private readonly AsignaturaRepositorio _asignaturaRepositorio;

        public List<Asignatura> Asignaturas { get; set; }
        public string ElementoABuscar { get; set; }

        public IndexModel(AsignaturaRepositorio asignaturaRepositorio)
        {
            _asignaturaRepositorio = asignaturaRepositorio;
        }

        public void OnGet(string elementoABuscar = "")
        {
            ElementoABuscar = elementoABuscar;
            Asignaturas = _asignaturaRepositorio.GetAsignaturasCurso(elementoABuscar).ToList();
        }
    }
}