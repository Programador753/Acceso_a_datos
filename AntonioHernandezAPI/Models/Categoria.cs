using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AntonioHernandezAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<NoticiaCategoria> NoticiaCategorias { get; set; }
        [NotMapped]
        public List<int> NoticiasSeleccionados { get; set; }
    }
}
