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
        public IFormFile Photo { get; set; } // Para manejar la subida de archivos
        public EditModel(IAlumnoRepositorio alumnoRepositorio, IWebHostEnvironment webHostEnvironment) // Inyección de dependencias
        {
            this.alumnoRepositorio = alumnoRepositorio;
            this.webHostEnvironment = webHostEnvironment;
        }
        public void OnGet(int? ID) // Es posible que ID sea nulo si no se proporciona en la URL
        {
            if (ID.HasValue)
            {
                alumno = alumnoRepositorio.GetAlumno(ID.Value);
            }
            else
            {
                alumno = new Alumno(); // Crear un nuevo objeto Alumno si ID es nulo
            }
        }

        public IActionResult OnPost(Alumno alumno) // Recibir el objeto Alumno actualizado desde el formulario
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

        private string ProcessUploadedFile() // Subir la foto al servidor
        {
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images"); // Carpeta donde se guardarán las fotos
                string filePath = Path.Combine(uploadsFolder, Photo.FileName); // Ruta completa del archivo
                using (var fileStream = new FileStream(filePath, FileMode.Create)) // Crear el archivo
                {
                    Photo.CopyTo(fileStream); // Copiar el contenido del archivo subido al nuevo archivo
                }
            }
            return Photo.FileName; // Devolver el nombre del archivo para guardarlo en la base de datos
        }


    }
}
