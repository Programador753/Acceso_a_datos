using Microsoft.AspNetCore.Mvc;
using RazorPages.Modelos;
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

        public IViewComponentResult Invoke(Curso? curso = null)
        {
            var resultado = AlumnoRepositorio.AlumnoPorCurso(curso);
            return View(resultado);
        }
    }
}
