using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AntonioAPI.Models
{
    public class AutorLibro
    {
        public int id { get; set; }

        [Column("autorId")]
        public int autorId { get; set; }

        [ForeignKey("autorId")]
        [JsonIgnore]
        public Autor autor { get; set; }

        [Column("libroId")]
        public int libroId { get; set; }

        [ForeignKey("libroId")]
        public Libro libro { get; set; }
    }
}
