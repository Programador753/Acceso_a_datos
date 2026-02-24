using Microsoft.EntityFrameworkCore;

namespace AntonioMVC.Models
{
    public class NoticiaCategoria
    {
        public int NoticiaId { get; set; }
        public Noticia noticia { get; set; }
        public int CategoriaId { get; set; }
        public Categoria categoria { get; set; }
    }
}
