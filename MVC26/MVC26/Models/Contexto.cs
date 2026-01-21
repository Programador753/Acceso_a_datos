using Microsoft.EntityFrameworkCore;
using static MVC26.Controllers.VehiculoController;

namespace MVC26.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehiculoTotal>().HasNoKey();
        }

        public DbSet<MarcaModelo> Marcas { get; set; }
        public DbSet<SerieModelo> Series { get; set; }
        public DbSet<VehiculoModelo> Vehiculos { get; set; }
        public DbSet<VehiculoTotal> VistaTotal { get; set; }
    }
}
