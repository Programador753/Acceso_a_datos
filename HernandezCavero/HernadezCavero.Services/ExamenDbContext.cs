using Microsoft.EntityFrameworkCore;
using HernandezCavero.Modelos;

namespace HernadezCavero.Services
{
    public class ExamenDbContext : DbContext
    {
        public ExamenDbContext(DbContextOptions<ExamenDbContext> options) : base(options)
        {
        }

        public DbSet<Venta> Ventas { get; set;}
        public DbSet<Categoria> Categorias { get; set;}
        public DbSet<Producto> Productos { get; set;}

    }
}
