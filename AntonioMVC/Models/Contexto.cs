using Microsoft.EntityFrameworkCore;

namespace AntonioMVC.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Noticia> Noticia { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<NoticiaCategoria> NoticiaCategoria { get; set; }
        public DbSet<ImagenNoticia> ImageneNoticia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NoticiaCategoria>().HasKey(nc => new { nc.NoticiaId, nc.CategoriaId });
        }
    }
}