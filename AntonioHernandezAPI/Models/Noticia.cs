using System.ComponentModel.DataAnnotations.Schema;

namespace AntonioHernandezAPI.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string? Guid { get; set; }
        public string? Titulo { get; set; }
        public string? Enlace { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string? Fuente { get; set; }
        public DateTime? FechaInsercion { get; set; }
        public List<NoticiaCategoria>? NoticiaCategorias { get; set; }
        [NotMapped]
        public List<int>? CategoriasSeleccionados { get; set; }
        public List<ImagenNoticia>? ImagenNoticias { get; set; }
    }
}
