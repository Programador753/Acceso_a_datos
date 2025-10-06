using System.ComponentModel.DataAnnotations;

namespace RazorPages.Modelos
{
    public class Alumno
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Obligatorio completar el nombre")]
        [MinLength(3, ErrorMessage = "El nombre tiene que tener un minimo de 3 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Obligatorio completar el e-mail")]
        [EmailAddress(ErrorMessage = "Expresión incorrecta")]
        [Display(Name = "Email de casa")]
        public String Email { get; set; }
        public string Foto { get; set; }
        public Curso? CursoID { get; set; }
    }
}
