using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntonioMVCv1.Models
{
    public class Autor
    {
        public int id { get; set; }
        public string? nom_autor { get; set; }
        public string? apellido1 { get; set; }
        public string? apellido2 { get; set; }
        [DataType(DataType.Date)]
        public DateTime? f_nac { get; set; }

        [DataType(DataType.Date)]
        public DateTime? f_def { get; set; }
        public int? paisId { get; set; }
        public Pais pais { get; set; }
        public string? foto { get; set; }
        public string? biografia { get; set; }
        public string? localidad { get; set; }

        public List<AutorPremio>? AutorPremios { get; set; }
        public List<AutorLibro>? AutorLibros { get; set; }
    }
}
