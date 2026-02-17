using System.Text.Json.Serialization;

namespace AntonioAPI.Models
{
    public class Libro
    {
        public int id { get; set; }
        public string? nom_libro { get; set; }
        public long? ano { get; set; }
        public string portada { get; set; }
        public string nom_archivo { get; set; }
        public string? pelicula { get; set; }
        public string? comentario { get; set; }
        public int? serieId { get; set; }
        public int indice_serie { get; set; }
        [JsonIgnore]
        public List<AutorLibro>? AutorLibros { get; set; }
    }
}
