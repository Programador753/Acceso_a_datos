using System.ComponentModel.DataAnnotations;

namespace RazorPages.Modelos
{
    public class Alumno
    {
        public int Id { get; set; } // Propiedad Id de tipo entero 
        [Required (ErrorMessage = "Obligatorio completar el nombre")] // Validación: campo obligatorio
        [MinLength(3, ErrorMessage = "El nombre tiene que tener un minimo de 3 caracteres")] // Validación: longitud mínima de 3 caracteres
        public string Nombre { get; set; } // Propiedad Nombre de tipo String 
        [Required(ErrorMessage = "Obligatorio completar el e-mail")] // Validación: campo obligatorio
        [EmailAddress(ErrorMessage = "Expresión incorrecta")] // Validación: formato de email correcto
        [Display(Name = "Email de casa")] // Etiqueta personalizada para la propiedad
        public String Email { get; set; } // Propiedad Email de tipo String
        public string Foto { get; set; } // Propiedad Foto de tipo String
        public Curso? CursoID { get; set; } // Propiedad CursoID de tipo Curso (enum)
    }
}
