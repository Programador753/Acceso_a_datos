using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AntonioHernandezAPI.Models
{
    public class ImagenNoticia
    {
        public int Id { get; set; }
        public int NoticiaId { get; set; }

        [JsonIgnore] // Agrega esto
        public Noticia noticia { get; set; }
        public string UrlImagen { get; set; }
        public string Tipo { get; set; }
    }
}
