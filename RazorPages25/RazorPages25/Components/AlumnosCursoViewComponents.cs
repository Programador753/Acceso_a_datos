using Microsoft.AspNetCore.Mvc;
using RazorPages.Services;

namespace RazorPages25.Components
{
    public class AlumnosCursoViewComponent : ViewComponent
    {
        public IAlumnoRepositorio AlumnoRepositorio { get; }
        public AlumnosCursoViewComponent(IAlumnoRepositorio alumnoRepositorio)
        {
            AlumnoRepositorio = alumnoRepositorio;
        }

        public IViewComponentResult Invoke()
        {
            var resultado = AlumnoRepositorio.AlumnoPorCurso();
            return View(resultado);
        }
    }
}
