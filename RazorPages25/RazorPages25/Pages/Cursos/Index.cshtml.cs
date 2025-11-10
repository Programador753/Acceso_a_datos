using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Modelos;

namespace RazorPages25.Pages.Cursos
{
    public class IndexModel : PageModel
    {
        public List<Curso> Cursos { get; set; }
        public void OnGet()
        {
            // Obtener todos los valores del enum Curso y convertirlos a una lista casteada de Curso
            // Usamos Enum.GetValues para obtener los valores del enum y luego los casteamos a Curso
            // Finalmente, convertimos el resultado a una lista usando ToList()
            // Castear significa convertir el tipo de dato a otro tipo compatible
            Cursos = Enum.GetValues(typeof(Curso)).Cast<Curso>().ToList();
        }
    }
}
