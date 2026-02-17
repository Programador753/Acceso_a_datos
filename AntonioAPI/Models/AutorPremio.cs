using System.Text.Json.Serialization;

namespace AntonioAPI.Models
{
    public class AutorPremio
    {
        public int id { get; set; }
        public int autorId { get; set; }
        [JsonIgnore]
        public Autor autor { get; set; }
        public int premioId { get; set; }
        public Premio premio { get; set; }
        public int anio { get; set; }
    }
}
