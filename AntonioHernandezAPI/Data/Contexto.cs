using AntonioHernandezAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AntonioHernandezAPI.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Noticia> Noticia { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<NoticiaCategoria> NoticiaCategoria { get; set; }
        public DbSet<ImagenNoticia> ImagenNoticia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NoticiaCategoria>().HasKey(nc => new { nc.NoticiaId, nc.CategoriaId });
            modelBuilder.Entity<ImagenNoticia>()
                .HasOne(i => i.noticia)
                .WithMany(n => n.ImagenNoticias)
                .HasForeignKey(i => i.NoticiaId)
                .OnDelete(DeleteBehavior.Cascade); // Las imágenes se eliminan si se elimina la noticia
        }
    }
}
