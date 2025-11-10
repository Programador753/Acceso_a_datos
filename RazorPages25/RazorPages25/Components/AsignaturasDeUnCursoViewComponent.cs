using Microsoft.AspNetCore.Mvc;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Components
{
    public class AsignaturasDeUnCursoViewComponent : ViewComponent
    {
        public AsignaturaRepositorio AsignaturaRepositorio { get; }
        public AsignaturasDeUnCursoViewComponent(AsignaturaRepositorio asignaturaRepositorio)
        {
            AsignaturaRepositorio = asignaturaRepositorio;
        }
        public IViewComponentResult Invoke(Curso curso)
        {
            var resultado = AsignaturaRepositorio.GetAsignaturasCurso(curso);
            return View(resultado);
        }

        
    }
}
