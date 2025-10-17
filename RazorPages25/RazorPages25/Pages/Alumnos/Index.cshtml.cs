using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Modelos;
using RazorPages.Services;
namespace RazorPages25.Pages.Alumnos
{
    public class IndexModel : PageModel
    {
        private readonly IAlumnoRepositorio alumnoRepositorio;
        public List<Alumno> Alumnos { get; set; }
        public string elementoABuscar { get; set; }
        public IndexModel(IAlumnoRepositorio alumnoRepositorio)
        {
            this.alumnoRepositorio = alumnoRepositorio;
        }
        public void OnGet(string elementoABuscar = "")
        {
            Alumnos = alumnoRepositorio.Busqueda(elementoABuscar).ToList();
        }
    }
}
