using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AntonioHernandezAPI.Models
{
    public class NoticiaCategoria
    {
        public int NoticiaId { get; set; }
        [JsonIgnore]
        public Noticia Noticia { get; set; }

        public int CategoriaId { get; set; }
        [JsonIgnore]
        public Categoria Categoria { get; set; }
    }
}

