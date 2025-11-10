using Microsoft.AspNetCore.Mvc;
using RazorPages.Services;
using RazorPages.Modelos;

namespace RazorPages25.Components
{
    public class ProfesoresDeUnCursoViewComponent : ViewComponent
    {
        public ProfesorRepositorio ProfesorRepositorio { get; }
        public ProfesoresDeUnCursoViewComponent(ProfesorRepositorio profesorRepositorio)
        {
            ProfesorRepositorio = profesorRepositorio;
        }
        public IViewComponentResult Invoke(Curso curso)
        {
            var resultado = ProfesorRepositorio.GetProfesoresCurso(curso);
            return View(resultado);
        }


    }
}
