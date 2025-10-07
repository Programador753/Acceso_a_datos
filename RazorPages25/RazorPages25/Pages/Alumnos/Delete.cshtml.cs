using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Pages.Alumnos
{
    public class DeleteModel : PageModel
    {
        private readonly IAlumnoRepositorio alumnoRepositorio;
        [BindProperty]
        public Alumno alumno { get; set; } // Propiedad para enlazar el formulario
        public DeleteModel(IAlumnoRepositorio alumnoRepositorio) // Inyección de dependencias
        {
            this.alumnoRepositorio = alumnoRepositorio;
        }
        public IActionResult OnPost()
        {
            alumnoRepositorio.Delete(alumno.Id);
            return RedirectToPage("Index");
        }
        public void OnGet(int id)
        {
            alumno = alumnoRepositorio.GetAlumno(id);
        }
    }
}
