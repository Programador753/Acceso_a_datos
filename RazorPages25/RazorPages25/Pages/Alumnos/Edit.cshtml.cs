using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Modelos;
using RazorPages.Services;

namespace RazorPages25.Pages.Alumnos
{
    public class EditModel : PageModel
    {
        private readonly IAlumnoRepositorio alumnoRepositorio;
        private readonly IWebHostEnvironment webHostEnvironment;
        public Alumno alumno;
        public IFormFile Photo { get; set; }
        public EditModel(IAlumnoRepositorio alumnoRepositorio, IWebHostEnvironment webHostEnvironment)
        {
            this.alumnoRepositorio = alumnoRepositorio;
            this.webHostEnvironment = webHostEnvironment;
        }
        public void OnGet(int ID)
        {
            alumno = alumnoRepositorio.GetAlumno(ID);
        }

        public IActionResult OnPost(Alumno alumno)
        {
            if (Photo != null)
            {
                if (alumno.Foto != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", alumno.Foto);
                    System.IO.File.Delete(filePath);
                }
                alumno.Foto = ProcessUploadedFile();
            }
            alumnoRepositorio.Update(alumno);
            return RedirectToPage("Index");
        }

        private string ProcessUploadedFile()
        {
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, Photo.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return Photo.FileName;
        }
    }
}
