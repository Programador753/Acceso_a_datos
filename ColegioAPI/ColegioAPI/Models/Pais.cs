using System.Text.Json.Serialization;

namespace ColegioAPI.Models
{
    public class Pais
    {
        public int ID { get; set; }
        public string nomPais { get; set; }
        [JsonIgnore]
        public ICollection<Alumno> Alumnos { get; set; }
    }
}
