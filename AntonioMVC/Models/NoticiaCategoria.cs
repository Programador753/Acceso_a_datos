using Microsoft.EntityFrameworkCore;

namespace AntonioMVC.Models
{
    public class NoticiaCategoria
    {
        public int NoticiaId { get; set; }
        public Noticia Noticia { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
