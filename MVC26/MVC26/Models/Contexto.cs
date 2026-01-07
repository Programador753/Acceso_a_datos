using Microsoft.EntityFrameworkCore;

namespace MVC26.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        public DbSet<MarcaModelo> Marcas { get; set; }
    }
}
