using Microsoft.EntityFrameworkCore;
using AntonioMVCv1.Models;
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
        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<Libro> Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeo explícito: Clase AutorPremio -> Tabla autor_premio
            modelBuilder.Entity<AutorPremio>().ToTable("autor_premio");

            modelBuilder.Entity<AutorLibro>().ToTable("autor_libro");
        }
    }
}
 