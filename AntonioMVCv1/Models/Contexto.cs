using Microsoft.EntityFrameworkCore;
namespace AntonioMVCv1.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Premio> Premios { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<AutorPremio> AutorPremios { get; set; }

    }
}
 