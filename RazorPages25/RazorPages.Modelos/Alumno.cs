using System.ComponentModel.DataAnnotations;

namespace RazorPages.Modelos
{
    public class Alumno
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public String Email { get; set; }
        public string Foto { get; set; }
        public Curso? CursoID { get; set; }
    }
}
