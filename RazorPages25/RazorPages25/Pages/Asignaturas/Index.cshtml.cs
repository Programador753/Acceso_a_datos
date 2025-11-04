using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Pages.Asignaturas
{
    public class IndexModel : PageModel
    {
        public AsignaturaRepositorio AsignaturaRepositorio { get; }
        public List<Asignatura> Asignaturas { get; set; }
        public IndexModel(AsignaturaRepositorio asignaturaRepositorio)
        {
            AsignaturaRepositorio = asignaturaRepositorio;
        }
        public void OnGet()
        {
            Asignaturas = AsignaturaRepositorio.GetAllAsignaturas().ToList();
        }
    }
}
