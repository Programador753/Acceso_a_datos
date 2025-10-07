using Microsoft.AspNetCore.Mvc;
using RazorPages.Services;

namespace RazorPages25.Components
{
    public class AlumnosCursoViewComponents : ViewComponent
    {
        public IAlumnoRepositorio AlumnoRepositorio { get; }
        public AlumnosCursoViewComponents(IAlumnoRepositorio alumnoRepositorio)
        {
            AlumnoRepositorio = alumnoRepositorio;
        }
        public IViewComponentResult Invoke()
        {
            var resultado = AlumnoRepositorio.AlumnosPorCurso();
            return View(resultado);
        }
    }
}
