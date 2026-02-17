using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AntonioAPI.Models
{
    public class Pais
    {
        public int id { get; set; }
        public string nom_pais { get; set; }
        public string bandera { get; set; }
        [NotMapped]
        public List<int> autorId { get; set; }
        [JsonIgnore]
        public List<Autor> autores { get; set; }
    }
}
